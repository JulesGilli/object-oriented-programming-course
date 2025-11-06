# Cours de Programmation Orientée Objet et Design Patterns en C#

## Introduction générale
Ce cours a pour objectif de fournir une compréhension solide des principes de la **Programmation Orientée Objet (POO)** et des **Design Patterns** en C#, avec un accent particulier sur leur **application dans Unity**.  
Chaque chapitre s’accompagne d’explications détaillées, d’exemples concrets et de bonnes pratiques professionnelles.

---

## Table des Matières

### Chapitre 1 — Introduction à la Programmation Orientée Objet (POO)

#### Objectif du chapitre
Comprendre les fondements de la POO, ses concepts centraux, ses avantages et son application dans des projets réels (notamment Unity).

#### Contenu détaillé
1. **Introduction générale**
    - Définition formelle de la POO
    - Contexte historique et apparition
    - Problèmes résolus et avantages (réutilisabilité, modularité, maintenabilité)
    - Comparaison avec la programmation procédurale

2. **Les Fondamentaux**
    - **1. Classe et objet** : définition, analogie, exemple C#
    - **2. Attributs et méthodes** : membres d’instance, visibilité, encapsulation de base
    - **3. Constructeurs** : rôle, surcharge, initialisation

3. **Les 4 piliers de la POO**
    - **4. Encapsulation** : protection de l’état interne, getters/setters
    - **5. Abstraction** : masquage des détails via interfaces et classes abstraites
    - **6. Héritage** : factorisation du code, classes de base et dérivées
    - **7. Polymorphisme** : surcharge, redéfinition, liaison dynamique

4. **Concepts avancés**
    - **8. Composition vs héritage** : préférence pour la composition
    - **9. Visibilité et portée** : `public`, `private`, `protected`, `internal`
    - **10. Couplage et Cohésion** : SRP, principes de bonne architecture

5. **Conclusion du Chapitre 1**
    - Récapitulatif des 4 piliers
    - Importance de la POO pour Unity (GameObject, MonoBehaviour, composants)
    - Transition vers les Design Patterns

---

### Chapitre 2 — Les Design Patterns

#### Objectif du chapitre
Découvrir les principaux **patrons de conception orientés objet**, comprendre leur rôle et leur application dans le développement logiciel et Unity.

#### Structure du chapitre
Les patterns sont classés selon leur rôle dans l’architecture : créationnels, structurels et comportementaux.

---

#### Partie I — Patterns Créationnels
1. **Singleton**
    - Instance unique et accessible globalement
    - Exemple : GameManager, AudioManager
    - Avantages, risques et bonnes pratiques

2. **Factory Method**
    - Délégation de la création d’objets
    - Exemple : générateur d’ennemis
    - Simplification et extension du code

3. **Builder**
    - Création d’objets complexes étape par étape
    - Exemple : configuration d’un personnage Unity
    - Fluent Builder et initialisation fluide

---

#### Partie II — Patterns Structurels
4. **Adapter**
    - Connecter des interfaces incompatibles
    - Exemple : interfaçage entre anciens et nouveaux systèmes Unity
    - Adaptateur d’objet vs adaptateur de classe

5. **Decorator**
    - Ajouter dynamiquement des comportements
    - Exemple : effets d’attaque (poison, feu, etc.)
    - Principe de composition dynamique

6. **Facade**
    - Simplifier l’accès à un système complexe
    - Exemple : gestionnaire global de jeu
    - Réduction du couplage et point d’entrée unique

---

#### Partie III — Patterns Comportementaux
7. **Observer**
    - Notification automatique d’événements
    - Implémentation via `event` / `delegate`
    - Exemple : mise à jour du HUD et gestion d’événements Unity

8. **Strategy**
    - Comportement interchangeable à l’exécution
    - Exemple : IA agressive, défensive ou aléatoire
    - Principe du choix dynamique d’algorithme

9. **State**
    - Gestion d’états internes et transitions
    - Exemple : joueur (repos, attaque, mort)
    - Comparaison avec Strategy

10. **Command**
    - Encapsulation d’actions comme objets
    - Exemple : système d’input, undo/redo, gameplay séquencé
    - Découplage entre demandeur et exécuteur

---

#### Conclusion du Chapitre 2
- Synthèse des trois familles de patterns
- Découplage, abstraction et modularité comme fondations
- Importance dans les architectures Unity modernes
- Transition vers la mise en pratique : mini-projet Unity

---

### Chapitre 3 — Mise en pratique (à venir)

#### Objectif du chapitre
Mettre en œuvre plusieurs patterns étudiés dans un **mini-projet Unity complet**, combinant POO et design patterns dans un cadre concret.

#### Structure prévisionnelle
1. **Présentation du projet**
    - Description du jeu et des objectifs techniques
2. **Architecture logicielle**
    - Organisation du code en couches modulaires
3. **Application des patterns**
    - Usage combiné de Singleton, Observer, State, Command, etc.
4. **Extension et maintenance**
    - Intégration de nouvelles fonctionnalités
    - Bonnes pratiques de refactorisation
5. **Conclusion finale**
    - Synthèse du parcours et des compétences acquises

---

## 📚 Références globales

- Gamma, Helm, Johnson, Vlissides — *Design Patterns: Elements of Reusable Object-Oriented Software*, Addison-Wesley, 1994.
- Freeman, Eric & Robson, Elisabeth — *Head First Design Patterns*, O’Reilly, 2021.
- Martin, Robert C. — *Clean Architecture*, Prentice Hall, 2017.
- [Refactoring Guru — Catalogue complet des Design Patterns](https://refactoring.guru/fr/design-patterns/catalog)
- [Microsoft Learn — Design Patterns en .NET](https://learn.microsoft.com/en-us/dotnet/architecture/grpc-for-wcf-developers/design-patterns)
- [Unity Learn — Programming Patterns](https://learn.unity.com/tutorial/programming-patterns)

---

> Ce cours constitue une base complète pour concevoir des architectures logicielles robustes, extensibles et maintenables, en alliant théorie et pratique dans un contexte C# et Unity.
