# Chapitre 1 — Introduction à la Programmation Orientée Objet (POO)

## Objectif du chapitre

Comprendre les fondements de la programmation orientée objet, ses concepts centraux, ses avantages dans le développement logiciel moderne, et son application dans un contexte C# et Unity.

---

## Introduction générale

### Définition formelle de la POO

La **programmation orientée objet (POO)** est un **paradigme de programmation** fondé sur le concept d’**objets**.
Un objet regroupe à la fois :

* des **données** (appelées *attributs* ou *champs*),
* et des **comportements** (appelés *méthodes*).

Chaque objet représente une **abstraction d’un élément du monde réel**, capable de stocker un état et d’interagir avec d’autres objets.
L’objectif de la POO est de structurer le code autour de ces entités logiques pour rendre les programmes plus modulaires, cohérents et extensibles.

En C#, tout repose sur la définition de **classes**, qui servent de modèles à la création d’objets. Ces objets interagissent entre eux pour former le comportement global du programme.

---

### Contexte historique

Les premiers concepts orientés objet apparaissent dans les années 1960 avec le langage **Simula 67**, souvent considéré comme le pionnier du paradigme.
Dans les années 1970, **Smalltalk** formalise davantage l’approche et introduit des notions comme les *messages* entre objets.
Ces idées inspireront ensuite des langages modernes tels que **C++**, **Java**, **C#**, **Python** ou **Ruby**.

Aujourd’hui, la majorité des environnements de développement professionnels (notamment .NET et Unity) utilisent la POO comme base de conception.

---

### Pourquoi la POO ?

La programmation procédurale (approche classique avant la POO) consiste à écrire des suites d’instructions manipulant des variables globales.
Cette approche devient difficile à maintenir lorsque le code grandit, car les dépendances entre fonctions et données se multiplient.

La POO a été conçue pour **modéliser les problèmes réels** de manière plus naturelle.
Plutôt que de raisonner en termes de fonctions isolées, elle invite à raisonner en termes d’**entités qui interagissent**, chacune possédant son propre état et ses propres comportements.

---

### Problèmes que la POO résout

1. **Complexité logicielle** : en structurant un projet en objets, chaque composant devient plus simple à comprendre et à modifier.
2. **Réutilisabilité** : les classes peuvent être réutilisées dans différents contextes.
3. **Modularité** : le code est organisé en modules indépendants (classes et namespaces).
4. **Extensibilité** : grâce à l’héritage et au polymorphisme, il est possible d’ajouter de nouvelles fonctionnalités sans altérer le code existant.
5. **Maintenabilité** : un code bien conçu en POO est plus facile à tester, à faire évoluer et à documenter.

---

### Avantages concrets dans un projet logiciel

| Avantage        | Description                                                | Exemple                                                                                        |
| --------------- | ---------------------------------------------------------- | ---------------------------------------------------------------------------------------------- |
| Réutilisabilité | Une même classe peut être utilisée dans plusieurs projets. | Une classe `Joueur` réutilisée dans différents jeux.                                           |
| Modularité      | Les objets sont isolés les uns des autres.                 | Un module `AudioManager` indépendant du module `UI`.                                           |
| Extensibilité   | On peut enrichir le code sans le réécrire.                 | Une classe `EnnemiVolant` dérivée de `Ennemi`.                                                 |
| Maintenabilité  | Le code est plus clair et moins sujet aux erreurs.         | Correction d’un bug dans une seule classe sans impacter le reste du projet.                    |
| Réalisme        | Le code reflète la structure du monde réel.                | Un objet `Voiture` avec des attributs (vitesse, couleur) et des méthodes (accélérer, freiner). |

---

### Cas d’utilisation typiques

La POO est aujourd’hui omniprésente dans les domaines suivants :

* **Jeux vidéo** : modélisation d’entités interactives (personnages, objets, environnements).
* **Applications métiers** : représentation d’entités comme les clients, produits ou commandes.
* **Systèmes embarqués** : encapsulation du comportement des composants matériels.
* **Outils et bibliothèques** : architecture modulaire et extensible (par exemple .NET, UnityEngine, Qt).
* **Modélisation de systèmes complexes** : simulation, IA, réseaux, physique, etc.

---

### Comparaison avec la programmation procédurale

| Aspect                | Programmation procédurale                          | Programmation orientée objet                |
| --------------------- | -------------------------------------------------- | ------------------------------------------- |
| Organisation du code  | Suite de fonctions manipulant des données globales | Ensemble de classes représentant des objets |
| Structure principale  | Fonctions et variables                             | Classes et objets                           |
| Réutilisation du code | Limitée, souvent par copie                         | Facilitée par l’héritage et la composition  |
| Maintenabilité        | Difficile dans les gros projets                    | Plus claire et évolutive                    |
| Exemple typique       | C, Pascal                                          | C#, Java, Python, C++                       |

---

### Transition vers la pratique

Les concepts théoriques de la POO deviennent concrets lorsqu’on les met en œuvre à travers quatre notions fondamentales :

1. **Classes et objets**
2. **Attributs et méthodes**
3. **Constructeurs**
4. **Les quatre piliers de la POO : Encapsulation, Abstraction, Héritage et Polymorphisme**

Ces principes sont omniprésents en C# et essentiels à la conception d’architectures solides, notamment dans Unity, où chaque entité du jeu est représentée par un **objet** attaché à un **GameObject**.

---

### Références utiles

* [Microsoft Learn — Fondamentaux C#](https://learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/)
* [Unity Learn — Scripting Fundamentals](https://learn.unity.com/pathway/junior-programmer)
* [Refactoring Guru — Principes de la POO](https://refactoring.guru/fr/design-patterns/oop-principles)
* Grady Booch, *Object-Oriented Analysis and Design with Applications*, Addison-Wesley, 2007.

