# Code du cours

Ce dossier contient une version **compilable et testée** de tous les exercices et projets du cours.

```
code/
├── CoursPOO.sln
├── Exercices/              ← code des exercices et des projets (généré à partir des fiches)
│   ├── Chapitre1/
│   └── Chapitre2/
├── Tests/
│   ├── ScenariosDesFiches.cs   ← généré : chaque scénario affiche-t-il la sortie annoncée ?
│   ├── ReglesMetier.cs         ← écrit à la main : invariants et règles importantes
│   └── SortieConsole.cs        ← outil de capture de la console
└── outils/
    └── generer_exercices.py    ← regénère Exercices/ et ScenariosDesFiches.cs
```

## Lancer les tests

Prérequis : le [SDK .NET 8](https://dotnet.microsoft.com/download) ou plus récent.

```bash
dotnet test code/CoursPOO.sln
```

Chaque exercice correspond à un espace de noms (`CoursPOO.Exercices.Ch1_04_Encapsulation`, `CoursPOO.Exercices.Ch2_Projet_Boulangerie`…) et à une classe `Scenario` dont la méthode `Executer()` reprend le test de la fiche.

## Après avoir modifié une fiche

Le code de `Exercices/` et le fichier `Tests/ScenariosDesFiches.cs` sont **générés** à partir du texte des fiches : ne les modifiez pas à la main. Modifiez la fiche, puis relancez :

```bash
python code/outils/generer_exercices.py
```

```bash
dotnet test code/CoursPOO.sln
```

Un test qui échoue signale qu’une solution ne compile plus, ou que la sortie annoncée dans la fiche ne correspond plus à la sortie réelle.

## Ce qui n’est pas testé ici

* **Les exemples Unity** : ils dépendent du moteur (`MonoBehaviour`, `Transform`…) et ne peuvent pas être compilés hors d’un projet Unity.
* **L’exercice de la fiche « 9. Visibilité et portée »** : il consiste justement à trouver les lignes qui **ne compilent pas**.
* **Les exemples courts du corps des fiches** : seuls les exercices et les projets sont extraits automatiquement.
