<template>
  <div class="profile-container">
    <h1>Profil Utilisateur</h1>
    <div v-if="user">
      <p>Nom: {{ user.name }}</p>
      <p>Email: {{ user.email }}</p>
      <!-- Ajoutez d'autres informations utilisateur ici -->
    </div>
    <div v-else-if="loading">
      <p>Chargement du profil...</p>
    </div>
    <div v-else>
      <p>Veuillez vous connecter pour voir votre profil.</p>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'ProfilePage',
  data() {
    return {
      user: null,
      loading: false,
      error: null
    }
  },
  created() {
    this.fetchUserProfile();
  },
  methods: {
    async fetchUserProfile() {
      this.loading = true;
      this.error = null;
      
      try {
        // Récupérer le token d'authentification (selon comment vous le stockez)
        const token = localStorage.getItem('authToken'); // ou depuis Vuex
        
        if (!token) {
          throw new Error('Non authentifié');
        }
        
        const response = await axios.get('https://votre-api.com/api/profile', {
          headers: {
            'Authorization': `Bearer ${token}`
          }
        });
        
        this.user = response.data;
      } catch (error) {
        console.error('Erreur lors de la récupération du profil:', error);
        this.error = error.response?.data?.message || error.message;
      } finally {
        this.loading = false;
      }
    }
  }
}
</script>

<style scoped>
.profile-container {
  padding: 20px;
  max-width: 600px;
  margin: 0 auto;
}
</style>