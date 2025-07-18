<template>
  <div class="login-container">
    <div class="left-section">
      <h1>
        Créez votre compte<br />
        pour générer et gérer<br />
        vos scénarios de test API<br />
        en toute simplicité.
      </h1>
      <div class="buttons">
        <button class="btn-login" @click="switchToLogin">Log in</button>
        <button class="btn-signup active">Sign up</button>
      </div>
    </div>

    <div class="right-section">
      <div class="login-box">
        <h2>Sign Up</h2>
        <hr />
        
        <input type="text" placeholder="Full Name" v-model="fullName" @input="validateName" />
        <span class="error-message" v-if="errors.fullName">{{ errors.fullName }}</span>
        
        <input type="email" placeholder="Email Address" v-model="email" @blur="checkEmailAvailability" />
        <span class="error-message" v-if="errors.email">{{ errors.email }}</span>
        
        <input type="password" placeholder="Password" v-model="password" @input="validatePassword" />
        <div class="password-strength" :class="passwordStrength">
          Sécurité : {{ passwordStrengthText }}
        </div>
        
        <input type="password" placeholder="Confirm Password" v-model="confirmPassword" @input="validatePasswordMatch" />
        <span class="error-message" v-if="errors.confirmPassword">{{ errors.confirmPassword }}</span>
        
        <div class="terms">
          <input type="checkbox" id="terms" v-model="acceptedTerms" />
          <label for="terms">I accept the Terms and Conditions</label>
        </div>
        
        <button 
          class="btn-submit" 
          @click="signup" 
          :disabled="!isFormValid"
          :class="{ 'btn-disabled': !isFormValid }"
        >
          <span v-if="loading">Creating account...</span>
          <span v-else>Create Account</span>
        </button>
        
        <p class="signup-link">
          Already have an account? <a href="#" @click.prevent="switchToLogin">Login Now.</a>
        </p>
      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'SignupPage',
  data() {
    return {
      fullName: '',
      email: '',
      password: '',
      confirmPassword: '',
      acceptedTerms: false,
      loading: false,
      errors: {
        fullName: '',
        email: '',
        password: '',
        confirmPassword: ''
      }
    };
  },
  computed: {
    passwordStrength() {
      if (!this.password) return 'weak';
      if (this.password.length < 8) return 'weak';
      if (/[A-Z]/.test(this.password) && /\d/.test(this.password)) return 'strong';
      return 'medium';
    },
    passwordStrengthText() {
      const strengthMap = { weak: 'Faible', medium: 'Moyenne', strong: 'Forte' };
      return strengthMap[this.passwordStrength];
    },
    isFormValid() {
      return (
        this.fullName &&
        !this.errors.fullName &&
        this.email &&
        !this.errors.email &&
        this.password &&
        !this.errors.password &&
        this.confirmPassword &&
        !this.errors.confirmPassword &&
        this.acceptedTerms &&
        this.passwordStrength !== 'weak'
      );
    }
  },
  methods: {
    
    validateName() {
      this.errors.fullName = this.fullName.length < 3 
        ? 'Minimum 3 caractères' 
        : '';
    },

    // Validation de l'email
    async validateEmail() {
      const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
      if (!emailRegex.test(this.email)) {
        this.errors.email = 'Email invalide';
        return;
      }

      try {
        const response = await axios.get(`/api/auth/check-email?email=${this.email}`);
        this.errors.email = response.data.exists ? 'Email déjà utilisé' : '';
      } catch (error) {
        this.errors.email = 'Erreur de vérification';
        console.error("API Error:", error);
      }
    },

    // Validation du mot de passe
    validatePassword() {
      this.errors.password = this.password.length < 8
        ? 'Minimum 8 caractères'
        : '';
      this.validatePasswordMatch();
    },

    // Confirmation du mot de passe
    validatePasswordMatch() {
      this.errors.confirmPassword = this.password !== this.confirmPassword
        ? 'Les mots de passe ne correspondent pas'
        : '';
    },

    // Soumission du formulaire
    async signup() {
      if (!this.isFormValid || this.loading) return;

      this.loading = true;
      try {
        const response = await axios.post('http://localhost:5216/api/auth/signup', {
          fullName: this.fullName,
          email: this.email,
          password: this.password
        });

        if (response.status === 201) {
          localStorage.setItem('userEmail', this.email);
          this.$router.push('/homepage');
        }
      } catch (error) {
        if (error.response) {
          // Erreur 4xx/5xx du serveur
          const message = error.response.data?.message || "Erreur lors de l'inscription";
          alert(message);
        } else {
          alert("Erreur réseau : " + error.message);
        }
      } finally {
        this.loading = false;
      }
    },

    switchToLogin() {
      this.$router.push('/login');
    }
  }
};
</script>

<style scoped>
/* Styles existants conservés */
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
  margin-bottom: 0.5rem;
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
  margin-top: 1rem;
}

.btn-disabled {
  background-color: #cccccc !important;
  cursor: not-allowed !important;
}

.signup-link {
  text-align: center;
  margin-top: 1rem;
}

.error-message {
  color: #ff4444;
  font-size: 0.8rem;
  display: block;
  margin-bottom: 0.5rem;
}

.password-strength {
  font-size: 0.8rem;
  margin-bottom: 0.5rem;
}

.password-strength.weak {
  color: #ff4444;
}

.password-strength.medium {
  color: #ffbb33;
}

.password-strength.strong {
  color: #00C851;
}

.terms {
  display: flex;
  align-items: center;
  margin: 1rem 0;
  font-size: 0.8rem;
}

.terms input {
  width: auto;
  margin-right: 0.5rem;
}
</style>