# Chapitre 2 — Les Design Patterns (Patrons de conception)

## Objectif du chapitre

Découvrir les **principaux patrons de conception orientés objet**, comprendre leurs **rôles**, leurs **avantages**, et savoir **comment les appliquer en C# et dans Unity** pour construire des architectures plus flexibles, maintenables et réutilisables.

---

## Introduction

### Définition

Un **Design Pattern** (ou **patron de conception**) est une **solution réutilisable à un problème de conception récurrent** dans le développement logiciel.
Il ne s’agit pas de code tout fait, mais d’un **modèle de structure ou d’interaction** entre classes et objets, que l’on peut adapter à des contextes variés.

Ces patrons codifient les **bonnes pratiques d’architecture logicielle**, en s’appuyant sur les **principes fondamentaux de la POO** : encapsulation, abstraction, héritage, et polymorphisme.

---

### Historique

Le concept de *Design Pattern* a été formalisé en 1994 par quatre ingénieurs — **Erich Gamma, Richard Helm, Ralph Johnson et John Vlissides**, surnommés le **Gang of Four (GoF)**, dans leur ouvrage fondateur :

> *Design Patterns: Elements of Reusable Object-Oriented Software* — Addison-Wesley, 1994.

Ce livre a défini **23 patrons classiques**, devenus la base de référence dans le monde de l’ingénierie logicielle.
Depuis, ces modèles ont été étendus et adaptés à divers langages, frameworks et paradigmes, notamment **C#**, **.NET**, et **Unity**.

---

### Pourquoi utiliser les Design Patterns ?

Les Design Patterns permettent de :

1. **Résoudre des problèmes récurrents** sans “réinventer la roue”.
2. **Structurer le code** de manière claire, extensible et maintenable.
3. **Améliorer la communication entre développeurs** grâce à un vocabulaire commun (ex. : “Utilise un *Observer* pour les notifications d’événements”).
4. **Favoriser la réutilisation du code** et la cohérence des architectures logicielles.
5. **Anticiper le changement** : les systèmes conçus avec des patterns sont plus adaptables aux évolutions futures.

---

### Analogie

Les Design Patterns sont aux développeurs ce que les **plans d’architecte** sont aux ingénieurs civils :

> Un ensemble de modèles éprouvés permettant de concevoir des structures solides, fiables et adaptées à leur usage.

De même qu’un architecte ne redessine pas une maison à chaque projet, un développeur ne devrait pas redéfinir les mêmes solutions pour des problèmes déjà connus.

---

### Classification des Design Patterns

Les 23 patrons décrits par le GoF sont généralement répartis en **trois grandes familles**, selon leur objectif principal :

| Famille             | Objectif principal                                                                          | Exemples de patrons                |
| ------------------- | ------------------------------------------------------------------------------------------- | ---------------------------------- |
| **Créationnels**    | Gérer la création d’objets de manière flexible et contrôlée.                                | Singleton, Factory Method, Builder |
| **Structurels**     | Définir comment les classes et objets s’assemblent pour former des systèmes plus complexes. | Adapter, Decorator, Facade         |
| **Comportementaux** | Gérer les interactions et la communication entre objets.                                    | Observer, Strategy, State, Command |

---

### Application dans C# et Unity

En **C#**, les patterns permettent de concevoir des architectures logicielles propres, indépendantes et extensibles.
Dans **Unity**, leur utilité est encore plus évidente :

* Le **Singleton** pour les gestionnaires globaux (GameManager, AudioManager, etc.).
* Le **Factory Method** ou le **Builder** pour la génération d’objets ou d’ennemis.
* Le **Observer** pour la gestion des événements (UI, score, etc.).
* Le **Strategy** ou le **State** pour les comportements d’IA ou de gameplay.
* Le **Command** pour la gestion d’entrées ou de systèmes d’actions.

Ces patrons deviennent ainsi des **outils concrets** pour structurer vos jeux et systèmes Unity avec efficacité et clarté.

---

## Structure du Chapitre

Nous allons explorer les Design Patterns selon leur famille :

### Partie I — Patterns Créationnels

1. Singleton
2. Factory Method
3. Builder

### Partie II — Patterns Structurels

4. Adapter
5. Decorator
6. Facade

### Partie III — Patterns Comportementaux

7. Observer
8. Strategy
9. State
10. Command

Chaque pattern sera étudié avec :

* sa **problématique**,
* son **principe de solution**,
* une **implémentation concrète en C#**,
* et une **application typique dans Unity**.

---

## Références utiles

* Gamma, Helm, Johnson, Vlissides — *Design Patterns: Elements of Reusable Object-Oriented Software*, Addison-Wesley, 1994.
* [Refactoring Guru — Catalogue des Design Patterns](https://refactoring.guru/fr/design-patterns/catalog)
* [Microsoft Learn — Design Patterns in .NET](https://learn.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/design-patterns)
* [Unity Learn — Programming Patterns](https://learn.unity.com/tutorial/programming-patterns)
