<template>
  <div class="poc-wrapper">
    <POCInterface />
  </div>
</template>

<script>
import POCInterface from '@/modules/poc-api-tester/src/App.vue'

export default {
  name: 'POCWrapper',
  components: { POCInterface }
}
</script>