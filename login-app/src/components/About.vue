<template>
  <div class="about-container">
    <div class="about-content">
      <h1>À propos de l'application</h1>
      <p class="welcome-text">
        Bienvenue sur TestAPI Builder – votre outil NoCode pour tester facilement des API REST.
      </p>
      <p class="description">
        Grâce à notre éditeur visuel basé sur des blocs, vous pouvez créer, exécuter et analyser vos scénarios de test sans écrire une seule ligne de code.
      </p>
      <p class="tagline">Le test d'API, réinventé pour tous</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'AboutPage'
};
</script>

<style scoped>

body{
    margin: 0;
    padding:0;
}
.about-container {
  padding: 2rem;
  height: auto; /* Hauteur naturelle */
  background-attachment: scroll !important; /* Annule le fixed */
}
.about-container {
  display: flex;
  justify-content: center;
  align-items: center;
  min-height: 100vh;
  padding: 0;
  font-family: 'Arial', sans-serif;
  padding-top: 1px; 
  background-image: url('~@/assets/background.jpg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  background-attachment: fixed;
  position: relative;
}

.about-container::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(255, 255, 255, 0.85);
  z-index: 0;
}

.about-content {
  max-width: 800px;
  text-align: center;
  padding: 40px;
  background-color: rgba(255, 255, 255, 0.9);
  border-radius: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  position: relative;
  z-index: 1;
}

h1 {
  color: #1d4ed8;
  margin-bottom: 30px;
  font-size: 2.2rem;
}

.welcome-text {
  font-size: 1.4rem;
  color: #334155;
  margin-bottom: 20px;
  font-weight: 600;
}

.description {
  font-size: 1.1rem;
  color: #475569;
  line-height: 1.6;
  margin-bottom: 25px;
}

.tagline {
  font-size: 1.3rem;
  color: #1d4ed8;
  font-style: italic;
  font-weight: 500;
}
</style>