<template>
  <div class="container">
    <div v-if="loading" class="text-center">Loading...</div>
    <div v-if="error" class="alert alert-danger text-center">{{ error }}</div>
    <div class="row">
      <div
        v-if="cheeseProducts.length > 0"
        class="col-lg-4 col-md-6 col-sm-12 mb-5"
        v-for="cheese in cheeseProducts"
        :key="cheese._id"
      >
        <div class="card" style="width: 100%; height: 100%;">
          <img
            :src="getImageUrl(cheese.cheese_product_image_url)"
            alt="Cheese Image"
            class="card-img-top"
          />
          <div class="card-body d-flex flex-column">
            <h5 class="card-title">{{ cheese.cheese_product_name }}</h5>
            <p class="card-text">{{ cheese.cheese_product_description }}</p>
            <p class="card-text"><strong>Origin:</strong> {{ cheese.cheese_product_origin }}</p>
            <p class="card-text"><strong>Price per Kilo:</strong> ${{ cheese.cheese_product_price_per_kilo }}</p>
            <p class="card-text"><strong>Stock:</strong> {{ cheese.cheese_product_stock }}</p>

            <div class="mt-auto">
              <p class="total-price">
                <strong>Total Price: $</strong>
                <span>{{ totalPrices[cheese._id] !== undefined ? totalPrices[cheese._id].toFixed(2) : '0.00' }}</span>
              </p>
              <label for="weightInput">Weight (kg):</label>
              <input
                type="number"
                class="form-control mb-2"
                id="weightInput"
                v-model.number="weights[cheese._id]"
                @input="calculateTotalPrice(cheese)"
                min="0"
                step="0.1"
              />
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
</template>

<script setup lang="ts">
import { ref, onMounted, defineComponent } from 'vue';

// Define the CheeseProduct interface
interface CheeseProduct {
  _id: string;
  cheese_product_image_url: string;
  cheese_product_name: string;
  cheese_product_description: string;
  cheese_product_origin: string;
  cheese_product_price_per_kilo: number;
  cheese_product_stock: number;
}

// Define reactive properties
const cheeseProducts = ref<CheeseProduct[]>([]);
const loading = ref<boolean>(true);
const error = ref<string | null>(null);
const weights = ref<Record<string, number>>({});
const totalPrices = ref<Record<string, number>>({});

// Fetch cheese products from API
onMounted(async () => {
  try {
    const response = await fetch('https://pz-cheeseria-rest-api-acd9gqcse0hwd6fx.australiacentral-01.azurewebsites.net/pz-cheeseria-rest-api/cheese_product/get_all_cheese_products');
    if (!response.ok) {
      throw new Error('Failed to fetch cheese products.');
    }
    const data = await response.json();
    cheeseProducts.value = data;
  } catch (err) {
    error.value = (err as Error).message; // Ensure proper error handling
  } finally {
    loading.value = false;
  }
});

// Function to get image URL
const getImageUrl = (imageFileName: string) => {
  return new URL(`../assets/${imageFileName}`, import.meta.url).href;
};

// Function to calculate total price based on weight
const calculateTotalPrice = (cheese: CheeseProduct) => {
  const weight = weights.value[cheese._id] || 0;
  const pricePerKilo = cheese.cheese_product_price_per_kilo;
  totalPrices.value[cheese._id] = weight * pricePerKilo;
};
</script>

<style scoped>
/* Add any scoped styles here */
</style>


<style scoped>
.btn-primary {
  color: white;
  font-weight: 600;
  border: none;
  background-color: #e6be00;
}

.btn-primary:hover {
  background-color: #ffd500;
}

.container {
  width: 100vh;
  margin-top: 80px;
}

.card {
  width: 50vh;
  border: none;
  border-radius: 0.5rem;
  overflow: hidden;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

.card-img-top {
  height: 200px;
  object-fit: cover;
}

.card-body {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  flex-grow: 1;
}

@media (max-width: 768px) {
  .card {
    width: 100%;
  }
}
</style>
