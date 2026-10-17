# Cours complet : Programmation Orientée Objet et Design Patterns en C#

> Un guide structuré et progressif pour apprendre la **Programmation Orientée Objet (POO)** et les **Design Patterns** en C#, avec un accent particulier sur leur **application dans Unity**.

---

## Objectif

Ce dépôt propose une **ressource complète et pédagogique** pour comprendre :

- les **principes fondamentaux** de la Programmation Orientée Objet ;
- les **Design Patterns classiques (GoF)** et quelques patterns incontournables du jeu vidéo ;
- leur **mise en œuvre en C# et dans Unity** (architecture de gameplay, modularité, maintenance).

## Comment suivre ce cours

- **Parcours conseillé :** lire les fiches dans l’ordre. Chaque fiche se termine par des liens *Précédent · Sommaire · Suivant*.
- **Format d’une fiche :** objectifs, explication, diagramme UML, exemple C#, application dans Unity, pièges courants, confusions fréquentes, exercice avec sortie attendue et solution.
- **Prérequis :** connaître les bases de la programmation (variables, conditions, boucles, fonctions). Aucune connaissance préalable de la POO n’est nécessaire.
- **Pour pratiquer :** tous les exemples C# hors Unity se testent dans un projet console (`dotnet new console`).

---

## Sommaire

### [Chapitre 1 — Introduction à la Programmation Orientée Objet](<Chapitre 1/Chapitre 1 — Introduction à la Programmation Orientée Objet.md>)

Définition de la POO, contexte historique, problèmes résolus et comparaison avec la programmation procédurale.

**Partie I — Les fondamentaux**

1. [Classe et objet](<Chapitre 1/Partie I — Les Fondamentaux/1. Classe et Objet.md>) — définition, analogie, instanciation
2. [Attributs et méthodes](<Chapitre 1/Partie I — Les Fondamentaux/2. Attributs et Méthodes.md>) — membres d’instance et de classe, état et comportement
3. [Constructeurs](<Chapitre 1/Partie I — Les Fondamentaux/3. Constructeurs.md>) — initialisation, surcharge, chaînage avec `this`

**Partie II — Les quatre piliers de la POO**

4. [Encapsulation](<Chapitre 1/Partie II — Les 4 piliers de la POO/4. Encapsulation.md>) — protection de l’état interne, propriétés, validation
5. [Abstraction](<Chapitre 1/Partie II — Les 4 piliers de la POO/5. Abstraction.md>) — classes abstraites, interfaces, comment choisir
6. [Héritage](<Chapitre 1/Partie II — Les 4 piliers de la POO/6. Héritage.md>) — classes de base et dérivées, `virtual`/`override`, `new` vs `override`
7. [Polymorphisme](<Chapitre 1/Partie II — Les 4 piliers de la POO/7. Polymorphisme.md>) — surcharge, redéfinition, liaison dynamique, conversions

**Partie III — Concepts avancés**

8. [Composition vs héritage](<Chapitre 1/Partie III — Concepts Avancés/8. Composition vs héritage.md>) — « est un » ou « a un »
9. [Visibilité et portée](<Chapitre 1/Partie III — Concepts Avancés/9. Visibilité et portée.md>) — modificateurs d’accès, portée et durée de vie
10. [Couplage et cohésion](<Chapitre 1/Partie III — Concepts Avancés/10. Couplage et Cohésion.md>) — responsabilité unique, injection de dépendances
11. [Principes SOLID](<Chapitre 1/Partie III — Concepts Avancés/11. Principes SOLID.md>) — les cinq principes, du carré-rectangle à l’inversion des dépendances

**Partie IV — Les outils du langage**

12. [Types valeur et types référence](<Chapitre 1/Partie IV — Les outils du langage/12. Types valeur et types référence.md>) — `class`, `struct`, `record`, copies et égalité
13. [Membres statiques, énumérations et namespaces](<Chapitre 1/Partie IV — Les outils du langage/13. Membres statiques, énumérations et namespaces.md>)
14. [Exceptions](<Chapitre 1/Partie IV — Les outils du langage/14. Exceptions.md>) — `throw`, `try`/`catch`, exceptions personnalisées, méthodes `TryXxx`
15. [Génériques](<Chapitre 1/Partie IV — Les outils du langage/15. Génériques.md>) — classes et méthodes génériques, contraintes `where`, covariance
16. [Délégués, événements et lambdas](<Chapitre 1/Partie IV — Les outils du langage/16. Délégués, événements et lambdas.md>) — `Action`, `Func`, captures, `event`

[Conclusion du chapitre 1](<Chapitre 1/Conclusion du chapitre 1.md>) — récapitulatif et importance de la POO dans Unity

### [Chapitre 2 — Les Design Patterns](<Chapitre 2/Chapitre 2 — Les Design Patterns.md>)

Définition, historique du *Gang of Four* et classification des patterns.

**Avant de commencer**

0. [Lire un diagramme UML](<Chapitre 2/0. Lire un diagramme UML.md>) — boîtes de classe, visibilités, relations, diagrammes de séquence et d’états

**Partie I — Patterns créationnels**

1. [Singleton](<Chapitre 2/Partie I — Patterns Créationnels/1. Singleton.md>) — une instance unique (GestionnaireJeu, GestionnaireAudio), et ses risques
2. [Factory Method](<Chapitre 2/Partie I — Patterns Créationnels/2. Factory Method.md>) — déléguer la création d’objets aux sous-classes
3. [Builder](<Chapitre 2/Partie I — Patterns Créationnels/3. Builder.md>) — construire des objets complexes étape par étape

**Partie II — Patterns structurels**

4. [Adapter](<Chapitre 2/Partie II — Patterns Structurels/4. Adapter.md>) — connecter des interfaces incompatibles
5. [Decorator](<Chapitre 2/Partie II — Patterns Structurels/5. Decorator.md>) — ajouter dynamiquement des comportements
6. [Facade](<Chapitre 2/Partie II — Patterns Structurels/6. Facade.md>) — simplifier l’accès à un système complexe

**Partie III — Patterns comportementaux**

7. [Observer](<Chapitre 2/Partie III — Patterns Comportementaux/7. Observer.md>) — notifications et événements (HUD, gameplay, UI)
8. [Strategy](<Chapitre 2/Partie III — Patterns Comportementaux/8. Strategy.md>) — algorithmes interchangeables (IA, armes)
9. [State](<Chapitre 2/Partie III — Patterns Comportementaux/9. State.md>) — comportement qui dépend d’un état interne
10. [Command](<Chapitre 2/Partie III — Patterns Comportementaux/10. Command.md>) — actions encapsulées, Undo/Redo, input

**Partie IV — Patterns complémentaires**

11. [Template Method](<Chapitre 2/Partie IV — Patterns complémentaires/11. Template Method.md>) — fixer l’ordre d’un algorithme, laisser varier ses étapes
12. [Composite](<Chapitre 2/Partie IV — Patterns complémentaires/12. Composite.md>) — traiter un élément et un groupe de la même façon
13. [Prototype](<Chapitre 2/Partie IV — Patterns complémentaires/13. Prototype.md>) — créer des objets en clonant un modèle (`Instantiate`)
14. [Object Pool](<Chapitre 2/Partie IV — Patterns complémentaires/14. Object Pool.md>) — recycler des objets coûteux (projectiles, particules)
15. [Flyweight](<Chapitre 2/Partie IV — Patterns complémentaires/15. Flyweight.md>) — partager les données communes à des milliers d’objets
16. [Mediator](<Chapitre 2/Partie IV — Patterns complémentaires/16. Mediator.md>) — faire communiquer des objets sans qu’ils se connaissent
17. [Iterator](<Chapitre 2/Partie IV — Patterns complémentaires/17. Iterator.md>) — parcourir une collection, `yield return` et coroutines

[Conclusion du chapitre 2](<Chapitre 2/Conclusion du chapitre 2.md>) — synthèse des familles de patterns et application dans Unity

### Chapitre 3 — Mise en pratique *(à venir)*

Un mini-projet Unity complet qui combine plusieurs patterns étudiés : State pour l’IA, Observer pour le HUD, Command pour les entrées, Object Pool pour les projectiles, Strategy pour les armes, Factory pour l’apparition des ennemis.

---

## Concepts clés abordés

- Programmation orientée objet : classes, objets, encapsulation, abstraction, héritage, polymorphisme
- Principes de conception : couplage et cohésion, composition, principes SOLID
- Outils du langage C# : types valeur et référence, génériques, exceptions, délégués et événements
- Design Patterns du *Gang of Four*, et patterns propres au jeu vidéo (Object Pool)
- Application concrète dans Unity : composants, ScriptableObjects, cycle de vie, Inspector

## Technologies

| Domaine | Technologie |
| --- | --- |
| Langage | C# |
| Framework | .NET / Unity |
| IDE recommandé | Visual Studio, Rider ou VS Code |
| Format | Markdown (diagrammes Mermaid, affichés nativement par GitHub) |

---

## Références globales

- Gamma, Helm, Johnson, Vlissides — *Design Patterns: Elements of Reusable Object-Oriented Software*, Addison-Wesley, 1994.
- Freeman, Eric & Robson, Elisabeth — *Head First Design Patterns*, O’Reilly, 2021.
- Martin, Robert C. — *Clean Architecture*, Prentice Hall, 2017.
- [Refactoring Guru — Catalogue complet des Design Patterns](https://refactoring.guru/fr/design-patterns/catalog)
- [Robert Nystrom — Game Programming Patterns (gratuit en ligne)](https://gameprogrammingpatterns.com/)
- [Unity — e-book « Level up your code with design patterns and SOLID »](https://unity.com/resources/design-patterns-solid-ebook)
