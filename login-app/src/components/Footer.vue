<template>
  <footer class="app-footer">
    <div class="footer-content">
      <p>&copy; 2025 NoCode API Tester. Tous droits réservés.</p>
      <nav class="footer-links">
        <router-link to="/a-propos">À Propos</router-link>
        <router-link to="/contact">Contact</router-link>
      </nav>
    </div>
  </footer>
</template>

<script>
export default {
  name: 'AppFooter'
}
</script>

<style scoped>
.app-footer {
  bottom: 0;
  left: 0;
  width: 100%;
  background-color: #e5f0ff;
  color:  #1d4ed8;
  padding: 20px 40px;
  text-align: center;
  z-index: 1000;
  border-radius: 0;
  margin-top: auto;
}

.footer-content {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.footer-links {
  display: flex;
  justify-content: center;
  gap: 20px;
}

.footer-links a {
  color:  #1d4ed8;
  text-decoration: none;
  font-weight: 500;
  transition: opacity 0.3s ease;
}

.footer-links a:hover {
  opacity: 0.8;
}
</style>
