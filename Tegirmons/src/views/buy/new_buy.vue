<template>
  <div>
    <div class="invoice-table">
    <!-- Header -->
    <div class="row header">
      <div class="cell">ID</div>
      <div class="cell">Sana</div>
      <div class="cell">Umumiy summa</div>
    </div>

    <!-- Body -->
    <div 
      v-for="invoice in invoices" 
      :key="invoice.id" 
      class="row" 
      @click="selectInvoice(invoice)"
    >
      <div class="cell">{{ invoice.id }}</div>
      <div class="cell">{{ invoice.date }}</div>
      <div class="cell">{{ invoice.total.toLocaleString() }} so‘m</div>
    </div>
  </div>

  <!-- Invoice Items -->
  <div v-if="selectedInvoice" class="invoice-items">
    <h3>Invoice #{{ selectedInvoice.id }} Items</h3>
    <div class="row header">
      <div class="cell">Mahsulot</div>
      <div class="cell">Miqdor</div>
      <div class="cell">Narxi</div>
    </div>
    <div 
      v-for="(item, index) in selectedInvoice.items" 
      :key="index" 
      class="row"
    >
      <div class="cell">{{ item.product_name }}</div>
      <div class="cell">{{ item.qty }}</div>
      <div class="cell">{{ item.price.toLocaleString() }} so‘m</div>
    </div>
  </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      invoices: [
        { id: 1, date: "2025-08-15", total: 250000, items: [
          { product_name: "Un", qty: 10, price: 50000 },
          { product_name: "Shakar", qty: 5, price: 25000 }
        ]},
        { id: 2, date: "2025-08-16", total: 180000, items: [
          { product_name: "Yog‘", qty: 2, price: 60000 },
          { product_name: "Choy", qty: 3, price: 20000 }
        ]}
      ],
      selectedInvoice: null
    }
  },
  methods: {
    selectInvoice(invoice) {
      this.selectedInvoice = invoice;
    }
  }
}
</script>

<style scoped>
.invoice-table, .invoice-items {
  display: flex;
  flex-direction: column;
  border: 1px solid #ddd;
  margin-bottom: 20px;
}

.row {
  display: flex;
  border-bottom: 1px solid #ddd;
  cursor: pointer;
}

.row.header {
  background: #f5f5f5;
  font-weight: bold;
}

.cell {
  flex: 1;
  padding: 10px;
  text-align: left;
}
</style>