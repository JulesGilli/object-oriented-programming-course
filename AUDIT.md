# Audit du cours POO & Design Patterns (C# / Unity)

*Audit réalisé le 17/10/2026. Il porte sur les 26 fichiers `.md` du dépôt et les prend comme source de référence complémentaire : les 14 PDF de la piscine Java Epitech T-JAV-500.*

---

## 1. Verdict global

| Axe | Note | En bref |
|---|---|---|
| Structure / progression | 🟢 7/10 | Le plan est logique (fondamentaux → piliers → concepts avancés → 3 familles de patterns) et les fiches sont homogènes. |
| Justesse technique | 🟠 5/10 | Il y a une quinzaine d'erreurs factuelles ou de bugs dans les exemples, dont certains sont exactement ce que le cours veut éviter. |
| Profondeur du contenu | 🟠 5/10 | Le cours reste souvent en surface, et des notions essentielles manquent (SOLID est pourtant promis dans le README, ainsi que les génériques, les types valeur/référence et les exceptions). |
| Pédagogie active | 🔴 2/10 | Il n'y a ni exercice, ni quiz, ni projet fil rouge, ni code exécutable. Le chapitre 3 « mise en pratique » n'existe pas. |
| Support visuel | 🔴 1/10 | Le cours ne contient aucun diagramme : ni UML, ni schéma de séquence, ni image. Il n'y a que du texte, des tableaux et du code. |
| Références | 🔴 3/10 | **16 liens sur 51 sont en 404**, et plusieurs liens fonctionnent mais pointent vers une page sans rapport. |
| Forme / édition | 🟠 5/10 | Le gabarit est trop répétitif, les identifiants mélangent français et anglais, la navigation entre les fichiers est absente et il y a des BOM parasites. |

**En résumé :** le squelette est bon et les sujets sont bien choisis. En revanche, le contenu a un côté « généré d'un bloc » : chaque fiche suit le même moule (Objectif → Définition → Code → Avantages → Inconvénients → Unity → Quand utiliser → Résumé → Références). Les exemples sont trop triviaux pour montrer *pourquoi* le pattern existe, et il y a des inexactitudes. Le plus gros chantier est le visuel et la pratique, qui sont aujourd'hui inexistants.

---

## 2. Problèmes transverses

### 2.1 Forme et organisation
- **Deux sommaires redondants** : le README et le fichier « Cours de Programmation Orientée Objet et Design Patterns en C#.md » disaient la même chose. *(Corrigé : ils sont fusionnés dans le [README](README.md), désormais seul sommaire, entièrement cliquable.)*
- **Aucune navigation** : il n'y a pas de liens entre les fiches (précédent / suivant / sommaire), et le README ne pointe vers aucun fichier.
- **Noms de fichiers peu pratiques** : ils contiennent des espaces, des tirets cadratins (—) et des accents, ce qui rend les URL GitHub illisibles. De plus, le tri alphabétique place `10. Couplage` avant `8. Composition`. Il faudrait passer au format `01-classe-et-objet.md`.
- **BOM UTF-8 au début de 23 fichiers sur 26** *(corrigé)*. C'est invisible dans l'éditeur, mais ça peut casser le rendu du premier titre et certains outils.
- **Mélange français/anglais dans le code** : les exemples C# sont en français (`Joueur`, `PrendreDegats`), les exemples Unity en anglais (`Player`, `TakeDamage`). Certains identifiants sont accentués (`Démarrer()` dans [6. Héritage.md](<Chapitre 1/Partie II — Les 4 piliers de la POO/6. Héritage.md>)). C'est légal en C#, mais c'est une mauvaise pratique. Il faut choisir une convention et s'y tenir (je recommande le code en anglais et les explications en français).
- **Gabarit trop rigide et redondant** : dans chaque pattern, *Avantages*, *Quand utiliser* et *Résumé* répètent les mêmes idées trois fois. Dans le même temps, il manque presque partout les sections qui font progresser : **pièges courants**, **patterns voisins / confusions fréquentes** et **exercice**.
- **Exemples Unity souvent recopiés de l'exemple C#** : c'est le même code traduit en anglais (Adapter, Decorator, State). L'exemple Unity devrait apporter quelque chose de spécifique : ScriptableObject, prefab, cycle de vie, Inspector.
- **Tableau des modificateurs d'accès répété 4 fois**, avec des variantes : dans les fiches 2, 4, 6 et 9.
- **Le README promet des choses absentes** : « Principes SOLID » (seul SRP est vraiment traité) et un « mini-projet Unity » (le chapitre 3 n'existe pas).

### 2.2 Support visuel : rien aujourd'hui
Le cours ne contient **aucun schéma**. C'est le levier n°1 pour rendre le cours « super clean » :
- **Diagramme de classes UML (Mermaid `classDiagram`)** pour chaque pilier et chaque pattern. GitHub l'affiche nativement et il n'y a aucune image à maintenir.
- **Diagramme de séquence** pour Observer, Command et Facade, c'est-à-dire là où ce qui compte est *l'ordre des appels*.
- **Diagramme d'états (`stateDiagram-v2`)** pour le pattern State : c'est littéralement fait pour ça.
- **Schéma mémoire pile/tas** pour classe/objet et les références : c'est là que les débutants décrochent.
- **Encadrés visuels** à la GitHub (`> [!TIP]`, `> [!WARNING]`, `> [!NOTE]`) pour sortir les pièges et les bonnes pratiques du texte courant.
- **Fiche récap illustrée** par chapitre (une page, un schéma).
- Une **mini-intro UML** est nécessaire avant le chapitre 2, pour que les diagrammes soient lisibles. La Day08 Java en contient une qu'on peut adapter.

### 2.3 Références : 16 liens morts sur 51
Je les ai vérifiés un par un par requête HTTP.

| Lien en 404 | Occurrences |
|---|---|
| `learn.microsoft.com/.../grpc-for-wcf-developers/design-patterns` | **9 fois** (presque tous les patterns) |
| `learn.unity.com/tutorial/programming-patterns` | 3 |
| `refactoring.guru/fr/design-patterns/oop-principles` | 2 |
| `refactoring.guru/fr/design-patterns/composition-over-inheritance` | 1 |
| `refactoring.guru/fr/refactoring/smells/coupling-cohesion` | 1 |
| `learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/` | 2 |
| `.../fundamentals/program-structure/variables`, `.../azure/.../layered` | 1 + 1 |
| `docs.unity3d.com/Manual/ComponentsAndInheritance.html` | 1 |
| 7 tutoriels `learn.unity.com/tutorial/...` (introduction-to-scripts, introduction-to-prefabs, interfaces-and-abstract-classes, input-system, finite-state-machine, events-and-delegates, ai-behaviour-and-state) | 7 |

Certains liens **fonctionnent mais sont hors sujet** :
- `BestPracticeUnderstandingPerformanceInUnity4-1` est présenté comme « Scripting Patterns » et « Architecture de projet ». C'est en réalité une page sur la *mémoire managée*.
- `ff650316` (le Singleton de Microsoft Patterns & Practices) est réutilisé pour « Factory Method » et « Command ».
- `using-properties` est présenté comme « Fluent Interfaces ».

**Ressources de référence absentes qu'il faudrait ajouter :**
- Robert Nystrom, *Game Programming Patterns* (gratuit en ligne). C'est LA référence patterns + jeu vidéo.
- L'e-book officiel Unity *Level up your code with design patterns and SOLID*.
- La doc Microsoft *C# language reference*, pour les nouveautés (record, init, required, primary constructors).

---

## 3. Erreurs factuelles et bugs dans le code

> Il faut les corriger en priorité : dans un cours, une erreur s'apprend aussi bien qu'une vérité.

### Chapitre 1

| # | Fichier | Problème | Correction |
|---|---|---|---|
| 1 | [3. Constructeurs.md:221](<Chapitre 1/Partie I — Les Fondamentaux/3. Constructeurs.md:221>) | Le cours dit que les constructeurs sont « pleinement applicables » aux **ScriptableObjects**. C'est faux : un ScriptableObject se crée avec `ScriptableObject.CreateInstance<T>()`, jamais avec `new`. | Retirer les ScriptableObjects de la phrase et ajouter un encadré « pièges Unity ». |
| 2 | [3. Constructeurs.md](<Chapitre 1/Partie I — Les Fondamentaux/3. Constructeurs.md>) | Il manque un point clé : **dès qu'on écrit un constructeur, le constructeur par défaut implicite disparaît**. | Ajouter un encadré `[!WARNING]`. |
| 3 | [3. Constructeurs.md:201](<Chapitre 1/Partie I — Les Fondamentaux/3. Constructeurs.md:201>) | Le code met des instructions de premier niveau *après* une déclaration de classe, ce qui provoque l'erreur de compilation CS8803. | Placer `var v = ...` avant la classe ou dans un `Main`. |
| 4 | [2. Attributs et Méthodes.md:95](<Chapitre 1/Partie I — Les Fondamentaux/2. Attributs et Méthodes.md:95>) | Le paragraphe juste au-dessus recommande de ne jamais exposer de champ public, puis l'exemple déclare `public string Nom`. De plus, `pointsDeVie` n'est jamais initialisé, donc il part de 0 et devient négatif. | Mettre l'exemple en cohérence avec ses propres bonnes pratiques. |
| 5 | [4. Encapsulation.md:128](<Chapitre 1/Partie II — Les 4 piliers de la POO/4. Encapsulation.md:128>) | **Bug de gameplay** : le setter *ignore silencieusement* une valeur négative. Un joueur à 10 PV qui prend 20 dégâts **reste à 10 PV et ne meurt jamais**. | `Math.Max(0, value)`, ou lever une `ArgumentOutOfRangeException`, en expliquant les deux options. |
| 6 | [4. Encapsulation.md:61](<Chapitre 1/Partie II — Les 4 piliers de la POO/4. Encapsulation.md:61>) | `Deposer(-50)` n'est pas validé et *retire* donc de l'argent. Sur un exemple dont le but est de montrer la validation, ça fait désordre. | Valider `montant > 0`. |
| 7 | [5. Abstraction.md:79](<Chapitre 1/Partie II — Les 4 piliers de la POO/5. Abstraction.md:79>) | Le cours affirme qu'une interface « ne fournit aucune implémentation ». C'est faux depuis C# 8 (méthodes par défaut, membres statiques). Unity le supporte. | Nuancer et ajouter un **tableau « classe abstraite vs interface : quand choisir quoi »**, qui manque complètement. |
| 8 | [5. Abstraction.md:161](<Chapitre 1/Partie II — Les 4 piliers de la POO/5. Abstraction.md:161>) | `IDamageable` et `IInteractable` sont présentées comme des « interfaces du moteur » Unity. **Elles n'existent pas dans Unity** : ce sont des conventions que l'on écrit soi-même. | Corriger : « interfaces que vous définissez vous-même ». |
| 9 | [5. Abstraction.md:160](<Chapitre 1/Partie II — Les 4 piliers de la POO/5. Abstraction.md:160>) | Le cours dit que `MonoBehaviour` « agit comme une classe de base abstraite ». Ce n'est pas une classe abstraite. | Reformuler. |
| 10 | [6. Héritage.md:79](<Chapitre 1/Partie II — Les 4 piliers de la POO/6. Héritage.md:79>) | Le tableau dit que les membres `private` ne sont « pas hérités ». C'est une confusion classique : ils **sont hérités** (ils existent dans l'objet) mais sont **inaccessibles**. | Corriger le tableau. |
| 11 | [6. Héritage.md](<Chapitre 1/Partie II — Les 4 piliers de la POO/6. Héritage.md>) | Il manque **`new` vs `override`** (le masquage de méthode). C'est LE piège C# : `Animal a = new Chat(); a.Crier();` ne fait pas la même chose dans les deux cas. | Ajouter une section avec un exemple de sortie. |
| 12 | [7. Polymorphisme.md:206](<Chapitre 1/Partie II — Les 4 piliers de la POO/7. Polymorphisme.md:206>) | Le cours dit qu'Unity appelle `Start()`/`Update()` par polymorphisme. **C'est faux** : ces méthodes ne sont pas `virtual` dans `MonoBehaviour`. Unity les trouve par réflexion, comme des « messages » (et c'est pour ça qu'on ne met pas `override`). | Corriger. C'est d'ailleurs un excellent contre-exemple pédagogique. |
| 13 | [7. Polymorphisme.md](<Chapitre 1/Partie II — Les 4 piliers de la POO/7. Polymorphisme.md>) | Le **polymorphisme paramétrique (génériques)** est absent, de même que l'upcast/downcast et `is`/`as`/pattern matching. | Ajouter. |
| 14 | [8. Composition vs héritage.md:197](<Chapitre 1/Partie III — Concepts Avancés/8. Composition vs héritage.md:197>) | « Composition + interfaces » est attribué au principe *Interface Segregation*. C'est plutôt **l'inversion de dépendance (DIP)**. | Corriger. |
| 15 | [9. Visibilité et portée.md:107](<Chapitre 1/Partie III — Concepts Avancés/9. Visibilité et portée.md:107>) | Le cours dit que la portée d'un bloc garantit « un contrôle précis de la mémoire ». Ce n'est pas le cas en C# (GC). Le cours parle aussi de « variables globales », qui n'existent pas en C#, et confond *portée* et *durée de vie* pour `static`. | Reformuler et ajouter un schéma portée / durée de vie. |
| 16 | [10. Couplage et Cohésion.md:163](<Chapitre 1/Partie III — Concepts Avancés/10. Couplage et Cohésion.md:163>) | La version « après refacto » dépend encore de **classes concrètes**, alors que la section précédente vient d'expliquer qu'il faut dépendre d'interfaces. | Utiliser `IAudioService`, etc. |

### Chapitre 2

| # | Fichier | Problème | Correction |
|---|---|---|---|
| 17 | [1. Singleton.md:93](<Chapitre 2/Partie I — Patterns Créationnels/1. Singleton.md:93>) | Le cours écrit « Depuis C# 6 ». L'initialiseur `static readonly` existe depuis C# 1. `Lazy<T>` n'est pas mentionné. | Corriger et présenter `Lazy<T>`. |
| 18 | [1. Singleton.md](<Chapitre 2/Partie I — Patterns Créationnels/1. Singleton.md>) | Côté Unity, il n'y a pas de remise à `null` dans `OnDestroy`, ni de mention du piège *Enter Play Mode Options* (sans domain reload, le champ statique survit d'une session à l'autre). Le débat « Singleton = anti-pattern ? » est à peine effleuré. | Ajouter les pièges et des alternatives (ScriptableObject, service locator, DI). |
| 19 | [2. Factory Method.md:172](<Chapitre 2/Partie I — Patterns Créationnels/2. Factory Method.md:172>) | `GetComponent<FabriqueGobelin>()` : le client **nomme le type concret**, ce qui annule tout l'intérêt du pattern. | `[SerializeField] FabriqueEnnemi fabrique;`, puis on choisit la fabrique dans l'Inspector. |
| 20 | [2. Factory Method.md](<Chapitre 2/Partie I — Patterns Créationnels/2. Factory Method.md>) | L'exemple ne montre pas l'essence du pattern : le *Creator* a de la logique métier (`Spawn()`) qui **appelle** la méthode fabrique. Il manque aussi la distinction **Simple Factory / Factory Method / Abstract Factory**, qui est la confusion n°1 des étudiants. Au passage, la Day08 Java fait justement une Simple Factory. Enfin, le tableau Résumé est mal formé (la première ligne de données est utilisée comme en-tête). | Réécrire l'exemple et ajouter un tableau comparatif. |
| 21 | [3. Builder.md:142](<Chapitre 2/Partie I — Patterns Créationnels/3. Builder.md:142>) | **La sortie annoncée est fausse** : `Nom` n'est jamais défini. La sortie réelle est `" le Guerrier (PV: 150…"` et non `"Guerrier le Guerrier"`. Par ailleurs, le builder réutilisé renvoie toujours **la même instance**. | Corriger le code, ou la sortie. |
| 22 | [3. Builder.md:273](<Chapitre 2/Partie I — Patterns Créationnels/3. Builder.md:273>) | Le cours vante le Builder comme « idéal pour l'immuabilité », mais le produit n'a que des **champs publics mutables**. `Build()` ne valide rien. Les alternatives C# modernes ne sont pas mentionnées : initialiseurs d'objets, `init`, `required`, `record` + `with`. Ce sont elles qui rendent le Builder souvent inutile en C#. | Rendre le produit immuable, valider dans `Build()` et ajouter une section « Avez-vous vraiment besoin d'un Builder en C# ? ». |
| 23 | [4. Adapter.md:72](<Chapitre 2/Partie II — Patterns Structurels/4. Adapter.md:72>) | L'exemple **n'adapte rien** : mêmes paramètres, même sémantique, seul le nom change. On ne voit pas l'intérêt. L'adaptateur de classe est cité mais jamais montré. | Prendre un vrai décalage : des coordonnées en tuiles vers `Vector3`, un SDK à callbacks vers une interface synchrone, ou des unités différentes. |
| 24 | [5. Decorator.md](<Chapitre 2/Partie II — Patterns Structurels/5. Decorator.md>) | Le cours cite les « bonus temporaires » comme cas d'usage, mais un décorateur est **difficile à retirer** d'une chaîne, et cette limite n'est pas dite. Un exemple qui *calcule* quelque chose (dégâts, stats) serait plus parlant qu'une suite de `Console.WriteLine`. | Prendre l'exemple Shield/FireSword de la Day08 Java (+10 PV, +3 dégâts) et ajouter un encadré sur les limites. |
| 25 | [6. Facade.md](<Chapitre 2/Partie II — Patterns Structurels/6. Facade.md>) | La façade Unity dépend de MonoBehaviour concrets, mais le cours affirme qu'elle « améliore les tests unitaires ». Il manque aussi la distinction Facade / Mediator / « God Object ». | Nuancer. |
| 26 | [7. Observer.md:266](<Chapitre 2/Partie III — Patterns Comportementaux/7. Observer.md:266>) | L'exemple Unity **s'abonne sans jamais se désabonner**, juste avant une section qui dit « toujours se désabonner ». Il faut montrer le couple `OnEnable`/`OnDisable`. | Corriger l'exemple. |
| 27 | [7. Observer.md:278](<Chapitre 2/Partie III — Patterns Comportementaux/7. Observer.md:278>) | « **Découplage fort** » est une formulation ambiguë qui peut être lue comme « couplage fort ». Par ailleurs, `OnTriggerEnter` est présenté comme un Observer : c'est un message moteur, pas un abonnement. La différence `event` / champ `delegate` public n'est pas expliquée. Le pattern *Event Channel* (ScriptableObject) est cité mais jamais montré. | Clarifier et ajouter un exemple Event Channel. |
| 28 | [8. Strategy.md:174](<Chapitre 2/Partie III — Patterns Comportementaux/8. Strategy.md:174>) | **Bug** : `MoveTowards(self.position, -target.position, …)` se dirige vers le *symétrique de la cible par rapport à l'origine*, et ne s'éloigne pas de la cible. | `self.position + (self.position - target.position).normalized`. |
| 29 | [8. Strategy.md](<Chapitre 2/Partie III — Patterns Comportementaux/8. Strategy.md>) | Il manque la Strategy idiomatique en C# (`Func<>`/lambda) et la Strategy en **ScriptableObject**, très utilisée dans Unity et qui se règle dans l'Inspector. `Debug.Log` dans `Update` inonde la console. | Ajouter ces deux variantes. |
| 30 | [9. State.md:187](<Chapitre 2/Partie III — Patterns Comportementaux/9. State.md:187>) | **Le cœur du pattern n'est pas montré** : ce sont les **états qui décident des transitions**, et c'est justement ce qui distingue State de Strategy. Ici, toutes les transitions viennent de l'extérieur, et on peut même « sortir » de l'état Mort (il logue « Impossible » puis sort quand même). La sortie console annoncée est fausse (« Zzz… » s'affiche à chaque frame). Le snippet `Update()` de la ligne 217 entre en conflit avec le `Update()` de `Player`. | Réécrire avec des transitions internes (par exemple `if (pv <= 0) player.ChangeState(Dead)`) et un `stateDiagram`. |
| 31 | [10. Command.md:126](<Chapitre 2/Partie III — Patterns Comportementaux/10. Command.md:126>) | `Undo()` n'est pas robuste : « éteindre » une lumière déjà éteinte puis faire `Undo()` l'*allume*, alors qu'il faudrait restaurer l'état précédent. `Annuler()` provoque une `NullReferenceException` si aucune commande n'a été définie. | Mémoriser l'état précédent dans la commande. |
| 32 | [10. Command.md:267](<Chapitre 2/Partie III — Patterns Comportementaux/10. Command.md:267>) | Le cours annonce « Undo/**Redo** », mais il n'y a pas de Redo. Les deux `ICommand` sont incohérents : celui de la version Unity n'a pas de `Undo()`, alors que `CommandManager` l'appelle. `FindObjectOfType` est **déprécié** (Unity 2023+). `Input.GetKeyDown` correspond à l'ancien Input Manager. | Ajouter une 2ᵉ pile pour Redo, utiliser `FindFirstObjectByType` et mentionner le nouvel Input System. |

---

## 4. Lacunes de contenu

### 4.1 Promesses non tenues
- **SOLID** : il est annoncé dans le README, mais seul SRP est traité. OCP, LSP, ISP et DIP sont juste cités en passant. **Il faut une fiche dédiée**, avec l'exemple classique Rectangle/Carré pour LSP.
- **Chapitre 3, mini-projet Unity** : il est annoncé comme « à venir » mais n'existe pas.

### 4.2 Notions POO essentielles absentes (à ajouter au chapitre 1)
| Notion | Pourquoi c'est indispensable | Présente dans les cours Java ? |
|---|---|---|
| **Types valeur vs référence** (`class` / `struct` / `record`) | C'est la base de tout en C#, et c'est critique dans Unity (`Vector3` est une struct). | — |
| **Membres `static`, classes statiques** | Ils ne sont qu'effleurés dans la fiche 2. | ✅ Day03 (Mars/Astronaut) |
| **Enums** | Ils sont omniprésents en jeu (états, types d'ennemis). | ✅ Day04 |
| **Namespaces** | Organisation du code et conflits de noms. | ✅ Day03 (`chocolate.Mars` vs `planet.Mars`) |
| **Exceptions** (personnalisées, throw/catch) | Lien direct avec l'encapsulation (rejeter un état invalide). | ✅ Day06, Day08, Day10 |
| **Génériques** (`List<T>`, contraintes `where T :`) | C'est le polymorphisme paramétrique. Il est utilisé partout (`GetComponent<T>`). | ✅ Day07 |
| **Délégués, événements, lambdas** | **Ils sont utilisés dans Observer sans avoir été introduits.** | — |
| **Interfaces vs classes abstraites : quand choisir** | C'est la question n°1 en entretien. | ✅ Day05 |
| **Égalité, `ToString`, `Equals`/`GetHashCode`** | Ce sont les classiques de la redéfinition. | — |
| **Propriétés modernes** (`init`, `required`, auto-props, constructeurs primaires) | C'est le C# d'aujourd'hui. | — |
| **Bases UML** | Indispensables pour lire les diagrammes du chapitre 2. | ✅ Day08 |
| **Loi de Déméter**, injection de dépendances | Ils prolongent naturellement couplage/cohésion. | — |
| **Tests unitaires** | Le cours répète « testable » sans jamais montrer un test. | ✅ Day09 (mini framework de test) |

### 4.3 Patterns pertinents pour Unity absents
Par ordre de priorité :
1. **Object Pool** : c'est *le* pattern de perf Unity, et Unity fournit `UnityEngine.Pool.ObjectPool<T>`.
2. **Composite** : la hiérarchie de Transforms est un Composite. Il y a un exercice Java tout prêt (Day08).
3. **Template Method** : il est proche de Factory Method et utilisé implicitement dans le Space Arena Java (`recoverAP`).
4. **Prototype** : `Instantiate(prefab)` est *littéralement* un Prototype. C'est un lien Unity très fort.
5. **Flyweight** : les ScriptableObjects de données partagées.
6. **Mediator / Event Bus**, **Service Locator** : ce sont les alternatives au Singleton.
7. **Iterator** : `IEnumerable`/`yield`, qui est au cœur des coroutines Unity.
8. *(Bonus)* **Abstract Factory**, pour clarifier la confusion avec Factory Method.

---

## 5. Ce qu'on peut reprendre des cours Java (T-JAV-500)

| Source Java | Ce qu'on récupère | Où dans le cours C# |
|---|---|---|
| **Day02 : le Gecko qui évolue** sur 8 exercices | Le principe d'**une classe fil rouge qui grandit d'un concept à la fois** (constructeur → getters → invariant énergie 0-100 → surcharge `hello(string)`/`hello(int)`). | Exercices de la Partie I |
| **Day03 : Mars & Astronaut** | Compteur statique d'ID, `chocolate.Mars` vs `planet.Mars` pour les namespaces, composition Phobos→Mars, agrégation `Team`. | Fiche static + fiche namespaces |
| **Day04 : le zoo** (Animal → Shark → GreatWhite) | `protected`, chaîne `base(...)`, `override` sélectif, `is`, « le requin ne peut pas se manger lui-même ». | Exercices Héritage |
| **Day05 : Space Arena** | **Capstone polymorphisme** : interface `IFighter` → classe abstraite `Unit` → `Monster`/`SpaceMarine` → 4 classes concrètes, avec un combat au tour par tour. | Projet de fin de chapitre 1 |
| **Day06 : RPG** | `sealed` (le `final` Java), exception personnalisée `WeaponException`. | Héritage + Exceptions |
| **Day07 : génériques** | `Solo<T>`, `Pair<T,V>`, `Min/Max<T> where T : IComparable<T>`, `Battalion`. | Nouvelle fiche Génériques |
| **Day08 : patterns** | Intro **UML** + **SOLID**. Exercices **Factory/Elf** (le lutin n'a pas le droit de faire `new`), **Composite** (Sentence/Word), **Observer** (suivi de commande), **Decorator** (Shield/FireSword). | Chapitre 2 |
| **Day09 : annotations + réflexion** | Mini framework de test fait maison, transposable en C# avec les **attributs** + `System.Reflection` (et ça démystifie xUnit/NUnit). | Bonus avancé |
| **Day10 : la Boulangerie** | **Projet final** : interfaces, classes abstraites, génériques (`Menu<TDrink, TFood>`), `Dictionary<Type,int>`, exceptions. | Projet de fin de chapitre 2, ou chapitre 3 |

**Pédagogie à copier :**
- **Sortie console attendue** fournie pour chaque exercice. On sait si on a juste.
- **Contraintes qui forcent le concept** : « `new` interdit dans `Elf` », « utilise `base`, ne réécris pas le constructeur ».
- **Questions guidées** plutôt que des réponses : « Y a-t-il une raison de stocker des PV négatifs ? ». C'est exactement ce qui aurait évité le bug n°5.
- **Encadré « Notions que tu dois savoir expliquer »** en tête de chaque fiche, comme les slides *trailer*.
- **Des scénarios avec de l'humour et une identité.** Le cours C# reste sur un Gobelin/Arme assez générique.

---

## 6. Gabarit de fiche proposé

```
# NN. Titre
> 🎯 À la fin de cette fiche, tu sais expliquer : … (3 à 5 points)
> ⏱️ 20 min · Prérequis : [fiche X](lien)

## Le problème          ← partir d'un code qui fait mal (avant)
## L'idée               ← 1 schéma Mermaid + 3 phrases
## En C#                ← code minimal, compilable, avec sortie réelle
## Dans Unity           ← un apport spécifique (Inspector, SO, cycle de vie), pas une traduction
## ⚠️ Pièges courants
## 🔀 À ne pas confondre avec…
## 🧪 Exercice           ← énoncé + sortie attendue + contrainte ; solution dans /solutions
## ✅ Récap en 1 tableau
---
← Précédent · Sommaire · Suivant →
```

Il faut aussi un dossier `code/` qui contienne une **solution .NET** (un projet console par fiche) et des **tests xUnit**. Ainsi, chaque extrait du cours est garanti compilable, ce qui aurait détecté les bugs 3, 21 et 28.

---

## 7. Plan d'action priorisé

### P0 : corriger (≈ ½ journée)
- [x] Corriger les 32 points de la section 3.
- [x] Remplacer les 16 liens morts et les 4 liens hors sujet (les 58 liens du cours répondent en 200).
- [x] Retirer les BOM, fusionner les deux sommaires et aligner le README sur le contenu réel.

### P1 : rendre le cours « clean » (≈ 2-3 jours)
- [ ] Adopter une convention unique de nommage (code en anglais, texte en français) et des noms de fichiers sans espaces ni accents.
- [x] Ajouter la navigation précédent / suivant / sommaire.
- [x] **Ajouter un diagramme Mermaid à chaque fiche** (classes, séquence ou états).
- [x] Ajouter une fiche « Lire un diagramme UML » avant le chapitre 2.
- [ ] Appliquer le nouveau gabarit : sections *Pièges* et *À ne pas confondre*.
- [x] Écrire la fiche **SOLID** (promise) : fiche 11 du chapitre 1.

### P2 : étoffer (≈ 1-2 semaines)
- [x] Nouvelles fiches au chapitre 1 : types valeur/référence, static & enums, exceptions, génériques, délégués/événements/lambdas (fiches 12 à 16). Le comparatif interface / classe abstraite a été ajouté à la fiche 5.
- [x] Nouveaux patterns : Template Method, Composite, Prototype, Object Pool (fiches 11 à 14 du chapitre 2).
- [ ] Patterns suivants : Flyweight, Mediator, Iterator.
- [ ] **Exercices avec sortie attendue** dans chaque fiche, adaptés de la piscine Java, avec des solutions séparées. *Fait pour les 10 nouvelles fiches (solutions dans des blocs repliables, code compilé et sorties vérifiées) ; reste à faire pour les 20 fiches d’origine.*
- [ ] **Projets de fin de chapitre** : Space Arena (ch. 1) et Boulangerie (ch. 2).
- [ ] Solution .NET + tests xUnit pour tout le code.

### P3 : chapitre 3
- [ ] Mini-projet Unity fil rouge, par exemple un petit jeu d'arène qui combine State (IA), Observer (HUD), Command (input + replay), Object Pool (projectiles), Strategy en ScriptableObject (armes) et Factory (spawn).

---

### Proposition de nouvelle arborescence
```
README.md                      ← sommaire unique, liens cliquables, parcours conseillé
00-introduction/
01-poo/
   01-classe-et-objet.md … 16-solid.md
02-uml/                        ← mini intro UML
03-design-patterns/
   creational/ structural/ behavioral/
04-projet-unity/
exercices/   solutions/
code/        ← solution .NET + tests
assets/      ← schémas exportés si besoin
```
