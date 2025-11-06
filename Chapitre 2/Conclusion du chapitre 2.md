# Conclusion du Chapitre 2 — Les Design Patterns

## Synthèse générale

Les **Design Patterns** constituent l’un des piliers de l’architecture logicielle moderne.
Ils offrent un **langage commun** et des **solutions éprouvées** à des problèmes récurrents rencontrés dans la conception orientée objet.

Tout au long de ce chapitre, nous avons vu que les Design Patterns ne sont **ni des recettes toutes faites**, ni des règles rigides :
ce sont des **principes de conception** permettant de **penser la structure du code**, de **réduire le couplage**, d’**améliorer la lisibilité**, et de **faciliter l’évolution** des systèmes logiciels.

---

## Les trois grandes familles de patterns

Les 23 patrons classiques définis par le *Gang of Four (GoF)* se regroupent selon leur **rôle dans l’architecture logicielle**.

| Famille             | Rôle principal                                                         | Exemples étudiés                   |
| ------------------- | ---------------------------------------------------------------------- | ---------------------------------- |
| **Créationnels**    | Gérer la manière dont les objets sont créés, instanciés et configurés. | Singleton, Factory Method, Builder |
| **Structurels**     | Définir comment les classes et objets s’organisent et collaborent.     | Adapter, Decorator, Facade         |
| **Comportementaux** | Décrire comment les objets interagissent et communiquent.              | Observer, Strategy, State, Command |

Chaque famille répond à un type de problème précis :

* Les **créationnels** s’intéressent à *quoi* créer et *comment* le créer.
* Les **structurels** s’intéressent à *comment organiser* et *connecter* les éléments du système.
* Les **comportementaux** s’intéressent à *comment faire collaborer* les objets.

---

## Les enseignements clés

### 1. L’importance du découplage

Les patterns (notamment *Observer*, *Strategy*, et *Command*) montrent qu’un bon design vise à **réduire les dépendances directes** entre classes.
Un système faiblement couplé est :

* plus facile à tester,
* plus modulaire,
* et plus résilient face aux changements.

### 2. La composition avant l’héritage

Des patterns comme *Decorator* et *Facade* illustrent le principe de **composition dynamique** :
plutôt que d’hériter de multiples classes, on combine des objets ayant chacun une responsabilité claire.

### 3. L’abstraction comme moteur de flexibilité

Les patterns reposent souvent sur des **interfaces** ou **classes abstraites**, qui servent de contrat entre composants.
Cela permet de **changer une implémentation sans impacter le reste du système**.

### 4. La cohérence architecturale

L’usage raisonné des patterns rend une architecture :

* **prévisible**,
* **documentée**,
* **partageable** entre développeurs.
  Un code bien structuré selon ces principes devient **auto-explicatif**.

---

## Importance pour le développement Unity

Dans **Unity**, la compréhension et l’application des Design Patterns permettent de concevoir :

* des **systèmes de gameplay modulaires** (IA, armes, interactions, etc.),
* des **architectures événementielles** robustes (Observer, Command),
* des **gestionnaires globaux** bien isolés (Singleton, Facade),
* des **comportements interchangeables** (Strategy, State),
* et des **pipelines de création** (Factory, Builder).

> En combinant la POO vue dans le Chapitre 1 et les patterns de ce chapitre, on acquiert les compétences fondamentales pour développer **des jeux professionnels, extensibles et maintenables**.

---

## Bonnes pratiques générales

1. **Comprendre le problème avant d’appliquer un pattern.**
   Les Design Patterns sont des outils, pas des obligations.

2. **Utiliser les interfaces pour découpler les systèmes.**
   Cela facilite les tests, la maintenance et la réutilisation du code.

3. **Favoriser la simplicité.**
   Le meilleur design est celui qui résout un problème sans le complexifier.

4. **Mélanger les patterns avec discernement.**
   Ils sont souvent complémentaires (*State + Observer*, *Factory + Singleton*, etc.), mais un excès peut nuire à la clarté.

5. **Documenter les intentions.**
   Le but d’un pattern est aussi de rendre les choix architecturaux explicites pour l’équipe.

---

## Pour aller plus loin

La maîtrise des Design Patterns ouvre la voie vers des concepts architecturaux plus avancés :

| Domaine                           | Concept clé                                      | Description                                                         |
| --------------------------------- | ------------------------------------------------ | ------------------------------------------------------------------- |
| **SOLID Principles**              | Responsabilité unique, Ouverture/Fermeture, etc. | Cinq principes pour concevoir un code modulaire et robuste.         |
| **DDD (Domain-Driven Design)**    | Modélisation orientée métier                     | Organisation du code autour du domaine fonctionnel.                 |
| **MVC / MVVM**                    | Architecture logicielle                          | Séparation des responsabilités entre données, logique et interface. |
| **ECS (Entity Component System)** | Architecture orientée données                    | Utilisée par Unity DOTS pour des performances accrues.              |

---

## Références recommandées

* Gamma, Helm, Johnson, Vlissides — *Design Patterns: Elements of Reusable Object-Oriented Software*, Addison-Wesley, 1994.
* Freeman, Eric & Robson, Elisabeth — *Head First Design Patterns*, O’Reilly, 2021.
* Martin, Robert C. — *Clean Architecture: A Craftsman’s Guide to Software Structure and Design*, Prentice Hall, 2017.
* [Refactoring Guru — Catalogue des Design Patterns](https://refactoring.guru/fr/design-patterns/catalog)
* [Microsoft Learn — .NET Design Patterns](https://learn.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/design-patterns)
* [Unity Learn — Programming Patterns](https://learn.unity.com/tutorial/programming-patterns)

