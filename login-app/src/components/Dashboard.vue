<template>
  <div class="dashboard-content">
    <!-- Hero Section -->
    <section class="hero">
      <h1>Testez vos API REST sans coder, rapidement et facilement</h1>
      <p>Une solution NoCode intuitive pour créer, automatiser et déployer vos scénarios de tests API.</p>
      <button @click="startTest" class="btn-primary">Commencer un test</button>
    </section>

    <!-- Pourquoi choisir notre app -->
    <section class="features">
      <h2>Pourquoi choisir notre app</h2>
      <div class="feature-list">
        <div class="feature-item">
          <i class="icon-visual-blocks"></i>
          <h3>Création visuelle</h3>
          <p>Construisez vos scénarios via une interface drag & drop intuitive.</p>
        </div>
        <div class="feature-item">
          <i class="icon-automation"></i>
          <h3>Automatisation</h3>
          <p>Exécutez vos tests de façon répétée et fiable sans effort manuel.</p>
        </div>
        <div class="feature-item">
          <i class="icon-deploy"></i>
          <h3>Déploiement simplifié</h3>
          <p>Déployez vos projets facilement grâce à Docker.</p>
        </div>
      </div>
    </section>

    <!-- Notre vision -->
    <section class="vision">
      <h2>Notre vision</h2>
      <p>
        Rendre le test d’API accessible à tous, même sans compétences techniques, en offrant un outil moderne,
        simple et puissant. Notre objectif est de démocratiser l'automatisation des tests grâce à une approche NoCode
        innovante.
      </p>
    </section>
  </div>
</template>

<script>
export default {
  name: 'AppDashboard',
  methods: {
  startTest() {
    this.$router.push('/dashboard/poc');
  }
}
  }
</script>

<style scoped>
.dashboard-content {
  padding: 40px;
  min-height: calc(100vh - 200px);
  width: 100%;
  box-sizing: border-box;
  background-color: #ffffff;
  border-radius: 20px;
  font-family: 'Poppins', Arial, sans-serif;
  color: #333;
}

/* Bleu */
.hero {
  text-align: center;
  padding: 50px 20px;
  background: linear-gradient(90deg, #2196f3, #1976d2);
  border-radius: 20px;
  margin-bottom: 50px;
  color: white;
}

.hero h1 {
  font-size: 2.8rem;
  margin-bottom: 15px;
}

.hero p {
  font-size: 1.2rem;
  margin-bottom: 30px;
}

.btn-primary {
  background-color: #1976d2;
  color: white;
  font-weight: bold;
  padding: 15px 40px;
  border: none;
  border-radius: 50px;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.btn-primary:hover {
  background-color: #0d47a1;
}

.features {
  margin-bottom: 50px;
}

.features h2 {
  text-align: center;
  margin-bottom: 40px;
  font-size: 2rem;
}

.feature-list {
  display: flex;
  justify-content: space-around;
  gap: 20px;
  flex-wrap: wrap;
}

.feature-item {
  flex: 1 1 250px;
  background: #e3f2fd;
  border-radius: 15px;
  padding: 25px;
  text-align: center;
  box-shadow: 0 2px 6px rgba(0,0,0,0.1);
}

.feature-item h3 {
  margin-top: 15px;
  font-size: 1.4rem;
  color: #1976d2;
}

/* Section vision */
.vision {
  background-color: #bbdefb;
  padding: 25px 40px;
  border-radius: 15px;
  text-align: center;
}

.vision h2 {
  margin-bottom: 20px;
  font-size: 1.8rem;
}

.vision p {
  font-size: 1.1rem;
  line-height: 1.6;
}
</style>
