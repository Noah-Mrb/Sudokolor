# 🧩 Sudokolor - README

Bienvenue dans **Sudokolor**, une application de Sudoku où les chiffres sont remplacés par des couleurs ! Avec des fonctionnalités pensées pour tous, y compris les daltoniens, **Sudokolor** offre une expérience unique et personnalisable pour résoudre des grilles de Sudoku colorées.

---

## 🚀 Fonctionnalités principales

- **Génération automatique de grilles colorées** : Créez des grilles réalisables selon plusieurs niveaux de difficulté.  
- **Retour en arrière** : Annulez votre dernier mouvement en un clic grâce à un bouton dédié.  
- **Validation des grilles** : L'application vérifie automatiquement si la solution est correcte.  
- **Sauvegarde et reprise de partie** : Reprenez une partie interrompue après fermeture de l’application.  
- **Personnalisation** : Jouez avec des chiffres ou un spectre réduit de couleurs.  
- **Partage de parties** : Générez et partagez un code unique représentant une grille.  
- **Multilingue** : Par défaut, l’application adopte la langue du système (FR/EN).
- **Historique** : Consultez les statistiques sur vos dix meilleures parties.
- **Aides** : Mettez en valeur les erreurs, et demandez des indices pour vous débloquer.
- **Contre-la-montre** : Affrontez le sablier sur vos grilles préférées pour faire le meilleur temps.

---

## 🔍 Détails des Sprints

### 🟢 Sprint 1 : Version Alpha  
> **25/10/2024**.  
**Objectif** : Proposer une version fonctionnelle de base pour jouer au Sudoku coloré.  

#### User Stories réalisées  
1. **US 1.1.0 - Lancer une partie** : Démarrez une nouvelle partie depuis le menu principal.  
2. **US 1.2.0 - Changer les valeurs des cases** : Modifiez les couleurs des cases via un panel de sélection.  

#### Tâches techniques  
- **TS 1.1.0 - Génération de grille** : Une grille aléatoire réalisable est générée avec 40 cases à compléter sur 81.  

---

### 🟠 Sprint 2 : Version Beta  
> **29/11/2024**.  
**Objectif** : Enrichir les fonctionnalités de base et améliorer l’expérience utilisateur.

#### User Stories réalisées  
1. **US 1.3.0 - Annuler action** : Un bouton permet d’annuler le dernier coup joué.  
2. **US 1.5.0 - Vérification de la grille** : La fin de partie est détectée automatiquement.  
3. **US 1.6.0 - Partage de la graine** : Partagez ou copiez le code unique d’une grille.  
4. **US 2.1.0 - Accessibilité des couleurs** : Ajustez les couleurs ou activez l’affichage des chiffres.  
5. **US 2.3.0 - Choix de la langue** : Changez la langue depuis les paramètres (FR/EN).  
6. **US 1.10.0 - Abandon** : Retournez au menu principal avec une option pour reprendre ou abandonner la partie.  

#### User Story Bonus  
- **US 1.9.0 - Sauvegarde d’état** : L’état de la partie est conservé après fermeture de l’application.  

#### Tâches techniques  
- **TS 1.4.0 - Nettoyage et optimisation** : Réduction de la complexité cyclomatique et augmentation de la couverture des tests.  
- **TS 1.5.0 - Adaptation d’affichage** : L’application s’adapte à divers formats d’écran.

---

### 🔴 Sprint 3 : Version Release
> **13/12/2024**.
**Objectif** : Implémenter les notions de niveaux et de score dans l'ensemble du jeu pour permettre une bonne expérience au plus grand nombre de joueurs.

#### User Stories réalisées
1. **US 1.4.1 - Nombre de cases restantes pour chaque couleurs** : Ajoute une bulle au-dessus de chaque couleur indiquant la quantité restante.
2. **US 1.8.0 - Historique des meilleures parties** : Accédez à l'historique de vos dix meilleures parties.
3. **US 2.0.0 - Choix de la difficulté** : Choisissez entre les niveaux facile, intermédiaire ou difficile.
4. **US 1.7.0 - Gestion du score** : Obtenez votre score qui prend en compte tous les paramètres de jeu.
5. **US 1.4.2 - Mode indice** : Activez-le pour vous sortir d'une passe difficile au prix de votre meilleur score. 
6. **US 2.2.0 - Contre-la-montre** : Jouez au mode contre-la-montre et terminez la grille avant la fin du chronomètre. 

#### Tâches techniques
- **TS 1.3.0 - Calcul des points** : Implémente le fonctionnement du calcul des points selon le temps, les aides demandées ou encore la difficulté de la partie.

---

## 📖 Structure des Epics

### 🎨 Epic 1 : Jouer au Sudoku  
Offrir une expérience de jeu engageante, fluide et stimulante avec des outils adaptés comme les indices, le suivi des scores, et la validation des grilles.  

### ⚙️ Epic 2 : Paramétrage  
Permettre aux joueurs de personnaliser l’application selon leurs préférences : choix des couleurs, mode daltonien, langue, et difficulté.

---

## Technologies du projet ⚙️

- Microsoft **.NET MAUI**

## Développeurs

- Romain CARD : Synxgz
- Nordine HIDA : NordineHida
- Noah MIRBEL : Noah-Mrb
- Valentin COLINDRE : ValentinColindre/Akt0o

---

## 🔗 Ressources

- **Dépôt GitHub** : [Lien vers le projet](https://github.com/dept-info-iut-dijon/S5A_A1_Coloku)  
- **Télecharger la dernière version** : [Lien vers l'APK](https://github.com/dept-info-iut-dijon/S5A_A1_Coloku/releases/tag/Beta)

