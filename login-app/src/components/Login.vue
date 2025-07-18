<template>
  <div class="login-container">
    <div class="left-section">
      <h1>
        Connectez-vous<br />
        pour créer vos<br />
        scénarios de test API<br />
        en toute sécurité.
      </h1>
      <div class="buttons">
        <button class="btn-login active">Log in</button>
        <button class="btn-signup">Sign up</button>
      </div>
    </div>

    <div class="right-section">
      <div class="login-box">
        <h2>Log In</h2>
        <hr />
        
        <input type="email" placeholder="Email Address" v-model="email" /> 
        <input type="password" placeholder="Password" v-model="password" />
        <button class="btn-submit" @click="login">Log in</button>
        <p class="signup-link">
             Don't have an account? <a href="#" @click.prevent="$router.push('/signup')">Signup Now.</a>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'LoginPage',
  data() {
    return {
      email: '',
      password: '',
      error: '' // Pour afficher les erreurs
    };
  },
  methods: {
    async login() {
      this.error = ''; // Réinitialise l'erreur

      // Validation simple
      if (!this.email || !this.password) {
        this.error = 'Email et mot de passe requis';
        return;
      }

      try {
        // Appel à l'API .NET
        const response = await axios.post(
          'http://localhost:5216/api/auth/login', // Remplace [PORT] par ton port .NET (ex: 5000)
          {
            email: this.email,
            password: this.password
          }
        );

        // Si succès, stocke le token et redirige
        localStorage.setItem('token', response.data.token);
        this.$router.push('/dashboard'); // Redirige vers la page après login

      } catch (err) {
        // Gestion des erreurs
        if (err.response) {
          // Erreur 4xx/5xx depuis le serveur
          this.error = err.response.data.message || 'Échec de la connexion';
        } else {
          this.error = 'Problème de réseau ou serveur indisponible';
        }
        console.error('Erreur:', err);
      }
    }
  }
};
</script>

<style scoped>
.login-container {
  display: flex;
  min-height: 100vh;
  font-family: sans-serif;
}

.left-section {
  flex: 1;
  padding: 5rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
}

.left-section h1 {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 2rem;
}

.buttons {
  display: flex;
  gap: 1rem;
}

.btn-login,
.btn-signup {
  padding: 0.7rem 1.5rem;
  border: none;
  border-radius: 20px;
  font-weight: bold;
  cursor: pointer;
}

.btn-login.active {
  background-color: #007bff;
  color: white;
}

.btn-signup {
  background-color: white;
  border: 1px solid #ccc;
}

.right-section {
  flex: 1;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: #f9f9f9;
}

.login-box {
  background: white;
  padding: 2rem;
  border-radius: 10px;
  width: 300px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.login-box h2 {
  text-align: center;
  margin-bottom: 1rem;
}

.login-box input {
  width: 100%;
  padding: 0.8rem;
  margin-bottom: 1rem;
  border-radius: 5px;
  border: 1px solid #ddd;
}

.btn-submit {
  width: 100%;
  background-color: #007bff;
  color: white;
  padding: 0.8rem;
  border: none;
  border-radius: 5px;
  cursor: pointer;
}

.signup-link {
  text-align: center;
  margin-top: 1rem;
}
</style>
