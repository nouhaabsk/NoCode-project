<template>
  <div id="app" class="app-root">
    <!-- Conteneur principal pour le contenu scrollable -->
    <div class="scrollable-container">
      <!-- Navbar - maintenant dans le flux normal -->
      <div v-if="showNavbar" class="dashboard-container">
        <nav class="navbar">
          <div class="nav-center">
            <router-link to="/profile" v-if="isAuthenticated">Profil</router-link>
            <router-link to="/dashboard" class="nav-link" active-class="active-link">
              Accueil
            </router-link>
            <router-link to="/dashboard/poc" class="nav-link" active-class="active-link">
              TEST API 
            </router-link>
            <router-link to="/historique">Historique</router-link>
            <router-link to="/a-propos">À Propos</router-link>
          </div>
          <div class="nav-right">
            <button class="logout-btn" @click="logout">Log Out</button>
          </div>
        </nav>
      </div>

      <!-- Contenu principal -->
      <main class="content-wrapper">
        <router-view v-if="!pocVisible" />
      </main>

      <!-- Footer -->
      <AppFooter v-if="showNavbar" />
    </div>

    <!-- Overlay POC (toujours fixe) -->
    <div v-if="pocVisible" class="poc-overlay">
      <POCInterface />
      <button @click="hidePOC" class="close-poc">×</button>
    </div>
  </div>
</template>

<script>
import POCInterface from '@/modules/poc-api-tester/src/App.vue';
import AppFooter from './components/Footer.vue';

export default {
  components: { POCInterface, AppFooter },
  data() {
    return {
      pocVisible: false
    };
  },
  computed: {
    showNavbar() {
      return this.$route.path !== '/login';
    }
  },
  methods: {
    showPOC() {
      this.pocVisible = true;
    },
    hidePOC() {
      this.pocVisible = false;
    },
    logout() {
      localStorage.removeItem('token');
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped>
/* Reset de base */
html, body, #app {
  height: 100%;
  margin: 0;
  padding: 0;
}

/* Structure principale */
.app-root {
  display: flex;
  flex-direction: column;
  min-height: 100vh;
}

/* Conteneur scrollable */
.scrollable-container {
  flex: 1;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
}

/* Navbar dans le flux normal */
.dashboard-container {
  background-color: #e5f0ff;
  padding: 20px;
  flex-shrink: 0; /* Empêche le rétrécissement */
}

/* Contenu principal */
.content-wrapper {
  flex: 1;
  background-color: #e5f0ff;
  padding: 0 20px 20px;
}

/* Footer reste en bas naturellement */
.app-footer {
  flex-shrink: 0;
}

.navbar {
  border-radius: 40px;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 10px 30px;
  margin-bottom: 20px;
  position: relative;
}

.nav-center {
  position: absolute;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  gap: 20px;
}


.nav-center a {
  font-weight: bold;
  color: #1d4ed8;
  text-decoration: none;
  position: relative;
  padding: 5px 10px;
  transition: color 0.3s ease;
}

.nav-center a::after {
  content: '';
  position: absolute;
  left: 0;
  bottom: -4px;
  width: 0%;
  height: 2px;
  background-color: #1d4ed8;
  transition: width 0.3s ease;
}

.nav-center a:hover {
  color: #2563eb; /* un bleu un peu plus foncé */
}

.nav-center a:hover::after {
  width: 100%;
}
.router-link-active::after {
  width: 100% !important;
}
.nav-right {
  position: absolute;
  right: 30px;
}

.logout-btn {
  display: flex;
  align-items: center;
  gap: 8px;
  background-color: transparent;
  color: #1d4ed8; /* Couleur cohérente avec votre thème */
  border: 2px solid #1d4ed8;
  padding: 8px 16px;
  border-radius: 30px; /* Bord arrondi pour un look moderne */
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
  margin-left: 15px; /* Espacement des autres éléments */
}

.logout-btn:hover {
  background-color: #1d4ed8;
  color: white;
  box-shadow: 0 4px 6px rgba(29, 78, 216, 0.2);
  transform: translateY(-2px);
}

.logout-btn:active {
  transform: translateY(0);
}

.logout-icon {
  display: flex;
  width: 16px;
  height: 16px;
}

.logout-icon svg {
  width: 100%;
  height: 100%;
  fill: currentColor;
  transition: transform 0.3s ease;
}

.logout-btn:hover .logout-icon svg {
  transform: translateX(2px); /* Effet de déplacement au hover */
}
.poc-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: white;
  z-index: 1000;
  overflow: auto;
}

.close-poc {
  position: fixed;
  top: 20px;
  right: 20px;
  background: #ef4444;
  color: white;
  border: none;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  font-size: 20px;
  cursor: pointer;
  z-index: 1001;
}

.nav-link {
  font-weight: bold;
  color: #1d4ed8;
  text-decoration: none;
  position: relative;
  padding: 5px 10px;
  transition: color 0.3s ease;
  cursor: pointer;
}
/* Styles POC overlay */
.poc-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: white;
  z-index: 1000;
  overflow: auto;
}

.close-poc {
  position: fixed;
  top: 20px;
  right: 20px;
  background: #ef4444;
  color: white;
  border: none;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  font-size: 20px;
  cursor: pointer;
  z-index: 1001;
}

.nav-link {
  font-weight: bold;
  color: #1d4ed8;
  text-decoration: none;
  position: relative;
  padding: 5px 10px;
  transition: color 0.3s ease;
  cursor: pointer;
}
</style>

