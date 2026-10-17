"""Génère le code compilable des exercices et des projets à partir des fiches du cours.

Usage (depuis la racine du dépôt) :
    python code/outils/generer_exercices.py

Pour chaque fiche qui contient un exercice (ou un projet), le script :
1. extrait les déclarations de la solution et le code du test ;
2. écrit un fichier C# dans code/Exercices/ (un espace de noms par fiche) ;
3. écrit code/Tests/ScenariosDesFiches.cs, qui vérifie que chaque scénario
   produit exactement la sortie annoncée dans la fiche.

Les fichiers générés ne doivent pas être modifiés à la main : modifiez la fiche,
puis relancez le script.
"""
import pathlib, re, unicodedata

RACINE = pathlib.Path(__file__).resolve().parents[2]
DOSSIER_EXERCICES = RACINE / "code" / "Exercices"
FICHIER_TESTS = RACINE / "code" / "Tests" / "ScenariosDesFiches.cs"
EN_TETE = "// Fichier généré par code/outils/generer_exercices.py à partir des fiches du cours.\n// Ne pas modifier à la main : modifiez la fiche, puis relancez le script.\n"

BLOC = re.compile(r"```(csharp)?\n(.*?)```", re.S)

# Déclarations définies plus haut dans la fiche et réutilisées par son exercice
PRELUDES = {
    "10. Command.md": "public interface ICommande { void Executer(); void Annuler(); }",
    "11. Principes SOLID.md": "public class Joueur { public string Nom { get; } public int Niveau { get; } public int Or { get; set; } public Joueur(string nom, int niveau) { Nom = nom; Niveau = niveau; } }",
    "12. Types valeur et types référence.md": "public struct PointS { public int X; public int Y; } public class Joueur { public string Nom; public int PointsDeVie; } public record Stats(int Vie, int Force);",
    "13. Membres statiques, énumérations et namespaces.md": "public enum TypeEnnemi { Gobelin, Orc, Dragon }",
    "13. Prototype.md": "public interface IPrototype<T> { T Cloner(); }",
    "14. Object Pool.md": "public class Projectile { private static int compteur; public int Id { get; } = ++compteur; public int DistanceParcourue { get; set; } }",
}
# Fiches dont le premier bloc de l’énoncé est le code « avant » à réécrire
SAUTER_PREMIER = {"10. Couplage et Cohésion.md", "11. Principes SOLID.md"}
# Exercice volontairement non compilable (trouver les erreurs de visibilité)
IGNORER = {"9. Visibilité et portée.md"}
# Les noms numérotés par un compteur statique (« Marine #3 ») dépendent de l’ordre des tests
NORMALISER_NUMEROS = {"Projet — Arène spatiale.md"}


def identifiant(chapitre, fichier):
    nom = fichier.removesuffix(".md")
    nom = unicodedata.normalize("NFKD", nom).encode("ascii", "ignore").decode()
    m = re.match(r"(\d+)\.\s*(.*)", nom)
    numero, titre = (f"{int(m.group(1)):02}", m.group(2)) if m else ("Projet", nom.replace("Projet", ""))
    mots = re.findall(r"[A-Za-z0-9]+", titre)
    return f"Ch{chapitre}_{numero}_" + "".join(w[:1].upper() + w[1:] for w in mots)


def extraire(fiche):
    t = fiche.read_bytes().decode("utf-8").replace("\r\n", "\n")
    if fiche.name.startswith("Projet"):
        solution = t[t.index("<details>"):t.index("</details>")]
        decl = [m.group(2) for m in BLOC.finditer(solution) if m.group(1)]
        b = list(BLOC.finditer(t[t.index("## Scénario final"):]))
        return decl, b[0].group(2), b[1].group(2)

    if "## 🧪 Exercice" not in t or fiche.name in IGNORER:
        return None
    debut = t.index("## 🧪 Exercice")
    fin = t.find("\n## ", debut + 5)
    ex = t[debut:fin]

    if "**Test attendu :**" in ex:
        idx = ex.index("**Test attendu :**")
        avant, apres = ex[:idx], ex[idx:]
        blocs_apres = list(BLOC.finditer(apres))
        test, attendu = blocs_apres[0].group(2), blocs_apres[1].group(2)
        decl = [m.group(2) for m in BLOC.finditer(avant) if m.group(1)]
        if fiche.name in SAUTER_PREMIER:
            decl = decl[1:]
        decl += [m.group(2) for m in blocs_apres[2:] if m.group(1)]
    else:
        # Exercice de prédiction : le code est dans l’énoncé, la sortie dans la solution
        test = next(m.group(2) for m in BLOC.finditer(ex) if m.group(1))
        solution = ex[ex.index("<details>"):]
        attendu = next(m.group(2) for m in BLOC.finditer(solution) if not m.group(1))
        decl = []
    return decl, test, attendu


def indenter(code, n):
    return "\n".join((" " * n + l) if l.strip() else "" for l in code.rstrip("\n").split("\n"))


def main():
    for ancien in DOSSIER_EXERCICES.glob("Chapitre*/*.cs"):
        ancien.unlink()

    tests = []
    fiches = sorted(p for p in RACINE.glob("Chapitre */**/*.md")
                    if "ArenePatterns" not in p.parts            # projet Unity du chapitre 3 (et son cache Library/)
                    and ("Partie" in str(p.parent.name) or p.name.startswith("Projet")))
    for fiche in fiches:
        chapitre = re.search(r"Chapitre (\d)", str(fiche.relative_to(RACINE))).group(1)
        resultat = extraire(fiche)
        if resultat is None:
            continue
        decl, test, attendu = resultat
        ns = identifiant(chapitre, fiche.name)

        # Méthodes écrites hors de toute classe : on les place dans la classe Scenario
        membres_libres, classes = [], []
        for d in decl:
            morceaux = re.split(r"(?m)^(?=public static (?!class\b))", d)
            classes.append(morceaux[0])
            membres_libres += morceaux[1:]
        if fiche.name in PRELUDES:
            classes.insert(0, PRELUDES[fiche.name])

        usings = sorted({l for c in classes + membres_libres for l in c.split("\n") if l.startswith("using ")})
        classes = ["\n".join(l for l in c.split("\n") if not l.startswith("using ")) for c in classes]
        membres_libres = ["\n".join(l for l in c.split("\n") if not l.startswith("using ")) for c in membres_libres]

        source = (EN_TETE + f"// Source : {fiche.relative_to(RACINE).as_posix()}\n\n" +
                  "".join(u + "\n" for u in usings) + ("\n" if usings else "") +
                  f"namespace CoursPOO.Exercices.{ns};\n\n" +
                  "\n\n".join(c.strip("\n") for c in classes if c.strip()) +
                  "\n\npublic static class Scenario\n{\n" +
                  "".join(indenter(m, 4) + "\n\n" for m in membres_libres) +
                  "    public static void Executer()\n    {\n" + indenter(test, 8) + "\n    }\n}\n")
        cible = DOSSIER_EXERCICES / f"Chapitre{chapitre}" / f"{ns}.cs"
        cible.parent.mkdir(parents=True, exist_ok=True)
        cible.write_text(source, encoding="utf-8", newline="\n")

        normaliser = "true" if fiche.name in NORMALISER_NUMEROS else "false"
        tests.append(
            f'    [Fact(DisplayName = "Ch. {chapitre} — {fiche.name.removesuffix(".md")}")]\n'
            f"    public void {ns}() => SortieConsole.Verifier(\n"
            f"        CoursPOO.Exercices.{ns}.Scenario.Executer, normaliserNumeros: {normaliser}, attendu:\n"
            f'        """\n{indenter(attendu, 8)}\n        """);\n')

    FICHIER_TESTS.write_text(
        EN_TETE + "\nnamespace CoursPOO.Tests;\n\n/// <summary>Vérifie que chaque scénario des fiches affiche exactement la sortie annoncée.</summary>\n"
        "public class ScenariosDesFiches\n{\n" + "\n".join(tests) + "}\n",
        encoding="utf-8", newline="\n")
    print(f"{len(tests)} scénarios générés dans {DOSSIER_EXERCICES.relative_to(RACINE)}")


if __name__ == "__main__":
    main()
