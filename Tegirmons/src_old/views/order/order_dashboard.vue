<template>
  <div class="p-3">
    <!-- Tablar -->
    <div class="mb-3">
      <div class="d-flex">
        <div 
          :class="{'bg-active_link': activeTab === 'statistics', 'order_link': activeTab !== 'statistics'}"
          @click="activeTab = 'statistics'"
          class="px-4 py-2 mr-2"
          style="cursor: pointer; border-radius: 5px 5px 0 0; font-size: 14px; font-weight: 600;">
          Statistika
        </div>
        <div 
          :class="{'bg-active_link': activeTab === 'clients', 'order_link': activeTab !== 'clients'}"
          @click="activeTab = 'clients'"
          class="px-4 py-2"
          style="cursor: pointer; border-radius: 5px 5px 0 0; font-size: 14px; font-weight: 600;">
          Clientlar
        </div>
      </div>
    </div>

    <div v-if="loading" class="text-center py-5">
      <div class="spinner-border text-primary" role="status">
        <span class="sr-only">Loading...</span>
      </div>
    </div>
    <div v-else>
      <!-- Statistika tab -->
      <div v-if="activeTab === 'statistics'">
        <!-- Sana filtrlari va statistika (mavjud kod) -->
        <!-- ... qolgan statistika kontenti shu yerda qolgan ... -->
      </div>

      <!-- Clientlar tab -->
      <div v-if="activeTab === 'clients'">
        <!-- Umumiy statistika cardlari -->
      <div class="row equal-height px-3 mb-3">
        <!-- Umumiy sum (UZS) -->
        <div class="col-3 p-1">
          <div class="card py-1 pt-2 px-2 main_kassa_poluchit balance">
            <span style="font-size: 13.5px;">Umumiy sum (UZS)</span>
            <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
              {{ formatNumber(totalSum) }}
            </span>
          </div>
        </div>
        <!-- Umumiy dollor (USD) -->
        <div class="col-3 p-1">
          <div class="card py-1 pt-2 px-2 main_kassa_bg balance">
            <span style="font-size: 13.5px;">Umumiy dollor (USD)</span>
            <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
              {{ formatNumber(totalDollor) }}💲
            </span>
          </div>
        </div>
        <!-- Mahsulotlar soni -->
        <div class="col-3 p-1">
          <div class="card py-1 pt-2 px-2 main_kassa_mah_qty balance">
            <span style="font-size: 13.5px;">Mahsulotlar soni</span>
            <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
              {{ productSummary.length }}
            </span>
          </div>
        </div>
        <!-- Clientlar soni -->
        <div class="col-3 p-1">
          <div class="card py-1 pt-2 px-2 main_kassa_client balance">
            <span style="font-size: 13.5px;">Clientlar soni</span>
            <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
              {{ clientList.length }}
            </span>
          </div>
        </div>
      </div>

      <!-- Mahsulotlar bo'yicha umumiy qty cardlari -->
      <div v-if="productSummary.length > 0" class="mb-3 px-2">
        <div class="row equal-height px-3">
          <div v-for="(product, index) in productSummary" :key="index" class="col-2 p-1">
            <div class="card py-1 pt-2 px-2 main_kassa_products balance">
              <span style="font-size: 13.5px;">{{ product.name }}</span>
              <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                {{ formatNumber(product.totalQty) }} {{ product.unit }}
              </span>
            </div>
          </div>
        </div>
      </div>

      <div class="p-2 card m-1">
        <table class="w-full border w-100 myTableuserSalaryList">
          <thead>
            <tr class="bg-gray-200 header py-3 info_client_header">
              <th style="padding: 6px 10px;" class="text-left" width="50">№</th>
              <th style="padding: 6px 10px;" class="text-left">{{$t('client_name')}}</th>
              <th style="padding: 6px 10px;" class="text-left">{{$t('phone_number')}}</th>
              <th style="padding: 6px 10px;" class="text-left">{{$t('sum')}} (UZS)</th>
              <th style="padding: 6px 10px;" class="text-left">{{$t('dollor')}} (USD)</th>
              <th style="padding: 6px 10px;" class="text-left">Mahsulotlar</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="(client, index) in clientList" :key="client.id">
              <td style="padding: 6px 10px;">{{ index + 1 }}</td>
              <td style="padding: 6px 10px;">
                <strong>{{ client.fio || client.name || '-' }}</strong>
              </td>
              <td style="padding: 6px 10px;">
                {{ client.phone_number || '-' }}
                <span v-if="client.phone_number1">, {{ client.phone_number1 }}</span>
                <span v-if="client.phone_number2">, {{ client.phone_number2 }}</span>
              </td>
              <td style="padding: 6px 10px;">
                <span class="text-success font-weight-bold">
                  {{ formatNumber(client.sum) }}
                </span>
              </td>
              <td style="padding: 6px 10px;">
                <span class="text-primary font-weight-bold">
                  {{ formatNumber(client.dollor) }}
                </span>
              </td>
              <td style="padding: 6px 10px;">
                <span v-if="client.products && client.products.length > 0">
                  {{ formatProducts(client.products) }}
                </span>
                <span v-else class="text-muted">-</span>
              </td>
            </tr>
            <tr v-if="clientList.length === 0">
              <td colspan="6" class="text-center p-4 text-muted">
                Ma'lumot topilmadi
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      </div>

      <!-- Statistika tab -->
      <div v-if="activeTab === 'statistics'">
        <!-- Sana filtrlari -->
        <div class="row mb-3 px-3">
          <div class="col-md-4">
            <label style="font-size: 12px; font-weight: 600; margin-bottom: 0px;">Boshlanish sanasi:</label>
            <input 
              type="date" 
              v-model="statsStartDate" 
              class="form-control"
              @change="yuklash_btn"
              style="height: 35px; font-size: 13px;"
            />
          </div>
          <div class="col-md-4">
            <label style="font-size: 12px; font-weight: 600; margin-bottom: 0px;">Tugash sanasi:</label>
            <input 
              type="date" 
              v-model="statsEndDate" 
              class="form-control"
              @change="yuklash_btn"
              style="height: 35px; font-size: 13px;"
            />
          </div>
          <div class="col-md-4 d-flex align-items-end justify-content-end">
            <div 
              class="main_kassa_btn_sml bg_col_blue w-100  d-flex justify-content-center align-items-center border rounded"
              style="height: 35px; cursor: pointer;"
              @click="yuklash_btn"
            >
              <small>🔄 Yuklash</small>
            </div>
          </div>
        </div>

        <div v-if="statsLoading" class="text-center py-5">
          <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
          </div>
        </div>

        <div v-else-if="statistics">
          <!-- Umumiy statistika cardlari -->
          <div class="row equal-height px-3 mb-3">
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_poluchit balance">
                <span style="font-size: 13.5px;">Jami zakazlar</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.totalOrders }}
                </span>
              </div>
            </div>
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_bg balance">
                <span style="font-size: 13.5px;">Yopilgan</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.closedOrders }}
                </span>
              </div>
            </div>
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_mah_qty balance">
                <span style="font-size: 13.5px;">Yopilmagan</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.notClosedOrders }}
                </span>
              </div>
            </div>
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_client balance">
                <span style="font-size: 13.5px;">To'langan</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.paidOrders }}
                </span>
              </div>
            </div>
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_products balance">
                <span style="font-size: 13.5px;">Yuklangan</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.loadedOrders }}
                </span>
              </div>
            </div>
            <div class="col-2 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_bg balance">
                <span style="font-size: 13.5px;">To'lanmagan</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ statistics.notPaidOrders }}
                </span>
              </div>
            </div>
          </div>

          <!-- Summa statistika -->
          <div class="row equal-height px-3 mb-3">
            <div class="col-3 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_poluchit balance">
                <span style="font-size: 13.5px;">Jami sum (UZS)</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ formatNumber(statistics.totalSum) }}
                </span>
              </div>
            </div>
            <div class="col-3 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_bg balance">
                <span style="font-size: 13.5px;">Jami dollor (USD)</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ formatNumber(statistics.totalDollor) }}💲
                </span>
              </div>
            </div>
            <div class="col-3 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_mah_qty balance">
                <span style="font-size: 13.5px;">To'langan sum (UZS)</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ formatNumber(statistics.paidSum) }}
                </span>
              </div>
            </div>
            <div class="col-3 p-1">
              <div class="card py-1 pt-2 px-2 main_kassa_client balance">
                <span style="font-size: 13.5px;">To'lanmagan sum (UZS)</span>
                <span class="text-right" style="font-size: 19px; display: block; margin-top: 5px;">
                  {{ formatNumber(statistics.unpaidSum) }}
                </span>
              </div>
            </div>
          </div>

          <!-- Top 10 mahsulotlar va Top 10 clientlar -->
          <div class="row">
            <!-- Top 10 mahsulotlar -->
            <div class="col-md-6">
              <div class="p-2 card m-1">
                <h6 class="mb-3 font-weight-bold px-2">Top 10 mahsulotlar</h6>
                <table class="w-100 myTableuserSalaryList">
                  <thead>
                    <tr class="bg-gray-200 header py-3 info_client_header">
                      <th style="padding: 6px 10px;" class="text-left" width="40">№</th>
                      <th style="padding: 6px 10px;" class="text-left">Mahsulot</th>
                      <th style="padding: 6px 10px; width: 150px;" class="text-left">Miqdor</th>
                      <th style="padding: 6px 10px; width: 150px;" class="text-left">Miqdor (kg)</th>
                      <th style="padding: 6px 10px; width: 100px;" class="text-left">Zakazlar</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr 
                      v-for="(product, index) in statistics.topProducts" 
                      :key="product.productId"
                      @click="openProductClientsModal(product)"
                      style="cursor: pointer;"
                      class="hover-row"
                    >
                      <td style="padding: 6px 10px; width: 40px; text-align: center;">
                        <span v-if="productsPage === 0 && index === 0" class="text-warning">
                          <i class="fas fa-medal" style="color:#ffd700;"></i>
                        </span>
                        <span v-else-if="productsPage === 0 && index === 1" class="text-secondary">
                          <i class="fas fa-medal" style="color:#c0c0c0;"></i>
                        </span>
                        <span v-else-if="productsPage === 0 && index === 2" class="text-brown">
                          <i class="fas fa-medal" style="color:#cd7f32;"></i>
                        </span>
                        <span v-else style="display: inline-block; min-width: 20px; text-align: center;">
                          {{ productsPage * 10 + index + 1 }}
                        </span>
                      </td>
                      <td style="padding: 6px 10px;">
                        <strong>{{ product.productName }}</strong>
                      </td>
                      <td style="padding: 6px 10px;">
                        {{ formatNumber(product.totalQty) }} {{ product.unit }}
                      </td>
                      <td style="padding: 6px 10px;">
                        {{ formatNumber(product.totalQtyKg) }} kg
                      </td>
                      <td style="padding: 6px 10px;">
                        {{ product.totalOrders }}
                      </td>
                    </tr>
                    <tr v-if="!statistics.topProducts || statistics.topProducts.length === 0">
                      <td colspan="5" class="text-center p-4 text-muted">
                        Ma'lumot topilmadi
                      </td>
                    </tr>
                  </tbody>
                </table>
                <!-- Pagination -->
                <div class="d-flex justify-content-center mt-2 mb-2">
                  <button 
                    v-for="page in 3" 
                    :key="page"
                    @click="productsPage = page - 1; fetchStatistics()"
                    :class="{'pagination-btn-active': productsPage === page - 1, 'pagination-btn': productsPage !== page - 1}"
                    class="pagination-circle-btn"
                  >
                    {{ page }}
                  </button>
                </div>
              </div>
            </div>

            <!-- Top 10 clientlar -->
            <div class="col-md-6">
              <div class="p-2 card m-1">
                <h6 class="mb-3 font-weight-bold px-2">Top 10 clientlar</h6>
                <table class="w-100 myTableuserSalaryList">
                  <thead>
                    <tr class="bg-gray-200 header py-3 info_client_header">
                      <th style="padding: 6px 10px; " class="text-left" width="40">№</th>
                      <th style="padding: 6px 10px; " class="text-left">Client</th>
                      <th style="padding: 6px 10px; width: 80px;" class="text-left">Zakazlar</th>
                      <th style="padding: 6px 10px; width: 130px;" class="text-left">UZS</th>
                      <th style="padding: 6px 10px; width: 120px;" class="text-left">USD</th>
                      <th style="padding: 6px 10px; width: 150px;" class="text-left">Сумма</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr 
                      v-for="(client, index) in statistics.topClients" 
                      :key="client.clientId"
                      @click="openClientProductsModal(client)"
                      style="cursor: pointer;"
                      class="hover-row"
                    >
                      <td style="padding: 6px 10px; width: 40px; text-align: center;">
                        <span v-if="clientsPage === 0 && index === 0" class="text-warning">
                          <i class="fas fa-medal" style="color:#ffd700;"></i>
                        </span>
                        <span v-else-if="clientsPage === 0 && index === 1" class="text-secondary">
                          <i class="fas fa-medal" style="color:#c0c0c0;"></i>
                        </span>
                        <span v-else-if="clientsPage === 0 && index === 2" class="text-brown">
                          <i class="fas fa-medal" style="color:#cd7f32;"></i>
                        </span>
                        <span v-else style="display: inline-block; min-width: 20px; text-align: center;">
                          {{ clientsPage * 10 + index + 1 }}
                        </span>
                      </td>
                      <td style="padding: 6px 10px;">
                        <strong>{{ client.clientName || '-' }}</strong>
                      </td>
                      <td style="padding: 6px 10px;">
                        {{ client.totalOrders }}
                      </td>
                      <td style="padding: 6px 10px;">
                        <span class="text-success font-weight-bold">
                          {{ formatNumber(client.totalSum) || 0}}
                        </span>
                      </td>
                      <td style="padding: 6px 10px;">
                        <span  class="text-primary ml-2">
                          {{ formatNumber(client.totalDollor) || 0}}💲
                        </span>
                      </td>
                      <td style="padding: 6px 10px;">
                        <span  class="text-primary ml-2">
                          {{ formatNumber(client.totalSumma) || 0}}
                            <!-- {{client}} -->
                        </span>
                      </td>
                    </tr>
                    <tr v-if="!statistics.topClients || statistics.topClients.length === 0">
                      <td colspan="4" class="text-center p-4 text-muted">
                        Ma'lumot topilmadi
                      </td>
                    </tr>
                  </tbody>
                </table>
                <!-- Pagination -->
                <div class="d-flex justify-content-center mt-2 mb-2">
                  <button 
                    v-for="page in 3" 
                    :key="page"
                    @click="clientsPage = page - 1; fetchStatistics()"
                    :class="{'pagination-btn-active': clientsPage === page - 1, 'pagination-btn': clientsPage !== page - 1}"
                    class="pagination-circle-btn"
                  >
                    {{ page }}
                  </button>
                </div>
              </div>
            </div>
          </div>

          <!-- Zakazlar soni bo'yicha chiziqli diagramma -->
          <div class="row mt-3 px-3">
            <div class="col-12">
              <div class="p-2 card m-1">
                <h6 class="mb-3 font-weight-bold px-2">Zakazlar sonining o'sish / kamayish diagrammasi</h6>
                <apexchart
                  :key="chartKey"
                  type="line"
                  height="260"
                  :options="chartOptions"
                  :series="chartSeries"
                />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Client mahsulotlari modal -->
    <modal-train 
      :show="showClientProductsModal" 
      headerbackColor="#64B0FB" 
      titlecolor="black" 
      :title="clientProductsData.clientName ? 'Mahsulotlar: ' + clientProductsData.clientName : 'Mahsulotlar'" 
      @close="showClientProductsModal = false" 
      width="70%"
    >
      <template v-slot:body>
        <div class="container-fluid p-3">
          <loader v-if="clientProductsLoading" />
          <div v-else>
            <div class="mb-2">
              <div class="row">
                <div class="col-md-6">
                  <div style="position: relative;">
                    <label style="position: absolute; top: -10px; left: 12px; font-size: 11px; font-weight: 600; background: white; padding: 0 4px; z-index: 1;">Boshlanish sanasi:</label>
                    <input 
                      type="date" 
                      v-model="clientProductsStartDate" 
                      class="form-control form-control-sm"
                      @change="fetchClientProducts"
                      style="height: 28px; font-size: 12px; padding-top: 4px;"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <div style="position: relative;">
                    <label style="position: absolute; top: -10px; left: 12px; font-size: 11px; font-weight: 600; background: white; padding: 0 4px; z-index: 1;">Tugash sanasi:</label>
                    <input 
                      type="date" 
                      v-model="clientProductsEndDate" 
                      class="form-control form-control-sm"
                      @change="fetchClientProducts"
                      style="height: 28px; font-size: 12px; padding-top: 4px;"
                    />
                  </div>
                </div>
              </div>
            </div>
            
            <div v-if="clientProductsData.products && clientProductsData.products.length > 0">
              <table class="w-100 myTableuserSalaryList">
                <thead>
                  <tr class="bg-gray-200 header py-3 info_client_header">
                    <th style="padding: 6px 10px;" class="text-left" width="50">№</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('product')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('qty')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">Кг</th>
                    <th style="padding: 6px 10px;" class="text-left">Zakazlar soni</th>
                  </tr>
                </thead>
                <tbody>
                  <tr 
                    v-for="(product, index) in clientProductsData.products" 
                    :key="product.productId"
                    :style="{ background: product.shitrixCode || 'transparent' }"
                  >
                    <td style="padding: 6px 10px;">{{ index + 1 }}</td>
                    <td style="padding: 6px 10px;">
                      <strong>{{ product.productName }}</strong>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ formatNumber(product.totalQty) }} {{ product.unit }}
                    </td>
                    <td style="padding: 6px 10px;">
                      <span class="text-success font-weight-bold">
                        {{ formatNumber(product.totalQtyKg) }} кг
                      </span>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ product.totalOrders }}
                    </td>
                  </tr>
                  <!-- Obshey qator -->
                  <tr class="bg-light font-weight-bold" style="border-top: 2px solid #333;">
                    <td style="padding: 6px 10px;" colspan="2">
                      <strong>Общие</strong>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ formatNumber(totalProductsQty) }}
                    </td>
                    <td style="padding: 6px 10px;">
                      <span class="text-success font-weight-bold">
                        {{ formatNumber(totalProductsKg) }} кг
                      </span>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ clientProductsData.totalOrders }}
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div v-else class="text-center p-4 text-muted">
              Ma'lumot topilmadi
            </div>
          </div>
        </div>
      </template>
    </modal-train>

    <!-- Product clients modal -->
    <modal-train 
      :show="showProductClientsModal" 
      headerbackColor="#64B0FB" 
      titlecolor="black" 
      :title="productClientsData.productName ? 'Clientlar: ' + productClientsData.productName : 'Clientlar'" 
      @close="showProductClientsModal = false" 
      width="70%"
    >
      <template v-slot:body>
        <div class="container-fluid p-3">
          <loader v-if="productClientsLoading" />
          <div v-else>
            <div class="mb-2">
              <div class="row">
                <div class="col-md-6">
                  <div style="position: relative;">
                    <label style="position: absolute; top: -10px; left: 12px; font-size: 11px; font-weight: 600; background: white; padding: 0 4px; z-index: 1;">Boshlanish sanasi:</label>
                    <input 
                      type="date" 
                      v-model="productClientsStartDate" 
                      class="form-control form-control-sm"
                      @change="fetchProductClients"
                      style="height: 28px; font-size: 12px; padding-top: 4px;"
                    />
                  </div>
                </div>
                <div class="col-md-6">
                  <div style="position: relative;">
                    <label style="position: absolute; top: -10px; left: 12px; font-size: 11px; font-weight: 600; background: white; padding: 0 4px; z-index: 1;">Tugash sanasi:</label>
                    <input 
                      type="date" 
                      v-model="productClientsEndDate" 
                      class="form-control form-control-sm"
                      @change="fetchProductClients"
                      style="height: 28px; font-size: 12px; padding-top: 4px;"
                    />
                  </div>
                </div>
              </div>
            </div>
            
            <div v-if="productClientsData.clients && productClientsData.clients.length > 0">
              <table class="w-100 myTableuserSalaryList">
                <thead>
                  <tr class="bg-gray-200 header py-3 info_client_header">
                    <th style="padding: 6px 10px;" class="text-left" width="50">№</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('client_name')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">{{$t('qty')}}</th>
                    <th style="padding: 6px 10px;" class="text-left">Кг</th>
                    <th style="padding: 6px 10px;" class="text-left">Zakazlar soni</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="(client, index) in productClientsData.clients" :key="client.clientId" >
                    <td style="padding: 6px 10px;">{{ index + 1 }}</td>
                    <td style="padding: 6px 10px;">
                      <strong :class="{'text-danger': client.clientName == `Noma'lum`}"> {{ client.clientName }}</strong>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ formatNumber(client.totalQty) }} {{ productClientsData.unit }}
                    </td>
                    <td style="padding: 6px 10px;">
                      <span class="text-success font-weight-bold">
                        {{ formatNumber(client.totalQtyKg) }} кг
                      </span>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ client.totalOrders }}
                    </td>
                  </tr>
                  <!-- Obshey qator -->
                  <tr class="bg-light font-weight-bold" style="border-top: 2px solid #333;">
                    <td style="padding: 6px 10px;" colspan="2">
                      <strong>Общие</strong>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ formatNumber(totalProductClientsQty) }}
                    </td>
                    <td style="padding: 6px 10px;">
                      <span class="text-success font-weight-bold">
                        {{ formatNumber(totalProductClientsKg) }} кг
                      </span>
                    </td>
                    <td style="padding: 6px 10px;">
                      {{ productClientsData.totalClients }}
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
            <div v-else class="text-center p-4 text-muted">
              Ma'lumot topilmadi
            </div>
          </div>
        </div>
      </template>
    </modal-train>
  </div>
</template>

<script>
import axios from 'axios';
import ApexChart from "vue-apexcharts";

export default {
  name: 'OrderDashboard',
  components: {
    apexchart: ApexChart
  },
  data() {
    return {
      clientList: [],
      loading: false,
      activeTab: 'statistics', // 'clients' yoki 'statistics'
      statistics: null,
      statsLoading: false,
      statsStartDate: null,
      statsEndDate: null,
      // Client products modal
      showClientProductsModal: false,
      clientProductsLoading: false,
      clientProductsData: {
        clientId: null,
        clientName: '',
        totalOrders: 0,
        products: []
      },
      clientProductsStartDate: null,
      clientProductsEndDate: null,
      // Product clients modal
      showProductClientsModal: false,
      productClientsLoading: false,
      productClientsData: {
        productId: null,
        productName: '',
        unit: '',
        shitrixCode: '',
        totalClients: 0,
        clients: []
      },
      productClientsStartDate: null,
      productClientsEndDate: null,
      // Pagination
      productsPage: 0, // 0, 1, 2 (3 ta sahifa)
      clientsPage: 0, // 0, 1, 2 (3 ta sahifa)
      // Chart data
      chartSeries: [
        {
          name: 'Zakazlar soni',
          data: []
        },
        {
          name: 'Mahsulotlar (kg)',
          data: []
        }
      ],
      chartKey: 0,
      chartOptions: {
        chart: {
          type: 'line',
          height: 260,
          toolbar: { show: false },
          zoom: { enabled: false }
        },
        colors: ['#008FFB', '#FF9800'],
        dataLabels: { enabled: false },
        stroke: { curve: 'smooth', width: 3 },
        xaxis: {
          categories: [],
          labels: { rotate: -45 }
        },
        yaxis: {
          labels: {
            formatter: (val) => this.formatNumber(val)
          }
        },
        tooltip: {
          y: {
            formatter: (val) => this.formatNumber(val)
          }
        }
      }
    };
  },
  mounted() {
    this.fetchClientsWithBalance();
    // Statistika uchun boshlang'ich sanalar
    const today = new Date();
    this.statsEndDate = today.toISOString().slice(0, 10);
    const monthAgo = new Date(today);
    monthAgo.setMonth(monthAgo.getMonth() - 1);
    this.statsStartDate = monthAgo.toISOString().slice(0, 10);
    // Sahifa ochilganda darhol statistika va diagramma yuklash
    this.fetchStatistics();
    this.fetchOrderChart();
  },
  watch: {
    activeTab(newTab) {
      if (newTab === 'statistics' && !this.statistics) {
        this.fetchStatistics();
        this.fetchOrderChart();
      }
    }
  },
  computed: {
    // Umumiy sum (UZS)
    totalSum() {
      return this.clientList.reduce((sum, client) => sum + (client.sum || 0), 0);
    },
    // Umumiy dollor (USD)
    totalDollor() {
      return this.clientList.reduce((sum, client) => sum + (client.dollor || 0), 0);
    },
    // Client products modal uchun umumiy qty
    totalProductsQty() {
      if (!this.clientProductsData.products || this.clientProductsData.products.length === 0) {
        return 0;
      }
      return this.clientProductsData.products.reduce((sum, product) => sum + (product.totalQty || 0), 0);
    },
    // Client products modal uchun umumiy kg
    totalProductsKg() {
      if (!this.clientProductsData.products || this.clientProductsData.products.length === 0) {
        return 0;
      }
      return this.clientProductsData.products.reduce((sum, product) => sum + (product.totalQtyKg || 0), 0);
    },
    // Product clients modal uchun umumiy qty
    totalProductClientsQty() {
      if (!this.productClientsData.clients || this.productClientsData.clients.length === 0) {
        return 0;
      }
      return this.productClientsData.clients.reduce((sum, client) => sum + (client.totalQty || 0), 0);
    },
    // Product clients modal uchun umumiy kg
    totalProductClientsKg() {
      if (!this.productClientsData.clients || this.productClientsData.clients.length === 0) {
        return 0;
      }
      return this.productClientsData.clients.reduce((sum, client) => sum + (client.totalQtyKg || 0), 0);
    },
    // Mahsulotlar bo'yicha umumiy qty
    productSummary() {
      const productMap = {};
      
      this.clientList.forEach(client => {
        if (client.products && Array.isArray(client.products)) {
          client.products.forEach(product => {
            const productName = product.product_name || 'Noma\'lum';
            const unit = product.unit || '';
            const key = `${productName}_${unit}`;
            
            if (!productMap[key]) {
              productMap[key] = {
                name: productName,
                unit: unit,
                totalQty: 0
              };
            }
            productMap[key].totalQty += product.qty || 0;
          });
        }
      });
      
      // Map ni array ga o'tkazish va qty bo'yicha sort qilish (ko'pdan kamga)
      return Object.values(productMap)
        .sort((a, b) => b.totalQty - a.totalQty);
    }
  },
  methods: {
    async refresh(){
        await this.fetchStatistics();
        await this.fetchOrderChart();
    },
    async yuklash_btn(){
        await this.fetchStatistics();
        await this.fetchOrderChart();

    },
    formatNumber(value) {
      if (value === null || value === undefined || isNaN(Number(value))) {
        return '0';
      }
      const num = Number(value);
      if (num % 1 === 0) {
        // Butun son bo'lsa, o'zgartirmaymiz
        return num.toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
      }
      // 2 ta onlik raqamgacha kesamiz
      return (Math.floor(num * 100) / 100).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
    },
    async fetchClientsWithBalance() {
      this.loading = true;
      try {
        const response = await axios.get(this.$store.state.hostname + '/TegirmonOrderClient/getClientsWithBalance');
        if (response.data && Array.isArray(response.data)) {
          this.clientList = response.data;
        } else {
          this.clientList = [];
        }
      } catch (error) {
        console.error('Error fetching clients with balance:', error);
        this.$toast.error('Ma\'lumotlarni yuklashda xatolik yuz berdi');
        this.clientList = [];
      } finally {
        this.loading = false;
      }
    },
    formatSum(value) {
      if (!value && value !== 0) return '0';
      return value.toFixed(2).toString().replace(/(\d)(?=(\d{3})+(\.(\d){0,2})*$)/g, '$1 ');
    },
    formatProducts(products) {
      if (!products || products.length === 0) return '-';
      
      return products.map(p => {
        const productName = p.product_name || 'Noma\'lum';
        const qty = p.qty || 0;
        const unit = p.unit || '';
        return `${productName} (${qty} ${unit})`;
      }).join(', ');
    },
    async fetchOrderChart() {
      try {
        let url = this.$store.state.hostname + '/TegirmonOrder/getOrderDailyStats';
        const params = [];

        if (this.statsStartDate) {
          params.push(`startDate=${this.statsStartDate}`);
        }
        if (this.statsEndDate) {
          params.push(`endDate=${this.statsEndDate}`);
        }

        if (params.length > 0) {
          url += '?' + params.join('&');
        }

        const response = await axios.get(url);
        if (response.data && Array.isArray(response.data) && response.data.length > 0) {
          const labels = response.data.map(d => {
            const dateObj = new Date(d.date);
            const day = String(dateObj.getDate()).padStart(2, "0");
            const month = String(dateObj.getMonth() + 1).padStart(2, "0");
            return `${day}.${month}`;
          });
          const ordersData = response.data.map(d => d.totalOrders || 0);
          const kgData = response.data.map(d => d.totalProductsKg || 0);

          this.chartSeries = [
            {
              name: 'Zakazlar soni',
              data: ordersData
            },
            {
              name: 'Mahsulotlar (kg)',
              data: kgData
            }
          ];
          // chartOptions ni yangi obyekt qilib beramiz (deep clone) va key ni oshiramiz
          this.chartOptions = Object.assign({}, this.chartOptions, {
            xaxis: Object.assign({}, this.chartOptions.xaxis, {
              categories: labels
            })
          });
          this.chartKey += 1;
        } else {
          this.chartSeries = [{
            name: 'Zakazlar soni',
            data: []
          }];
        }
      } catch (error) {
        console.error('Error fetching order chart:', error);
      }
    },
    async fetchStatistics() {
      this.statsLoading = true;
      try {
        let url = this.$store.state.hostname + '/TegirmonOrder/getGeneralStatistics';
        const params = [];
        
        if (this.statsStartDate) {
          params.push(`startDate=${this.statsStartDate}`);
        }
        if (this.statsEndDate) {
          params.push(`endDate=${this.statsEndDate}`);
        }
        // Pagination parametrlari
        params.push(`productsPage=${this.productsPage}`);
        params.push(`clientsPage=${this.clientsPage}`);
        
        if (params.length > 0) {
          url += '?' + params.join('&');
        }
        
        const response = await axios.get(url);
        if (response.data) {
          this.statistics = response.data;
        } else {
          this.statistics = null;
        }
      } catch (error) {
        console.error('Error fetching statistics:', error);
        this.$toast.error('Statistikani yuklashda xatolik yuz berdi');
        this.statistics = null;
      } finally {
        this.statsLoading = false;
      }
    },
    async openClientProductsModal(client) {
      this.clientProductsData.clientId = client.clientId;
      this.clientProductsData.clientName = client.clientName;
      // Sana filtrlari - statistika sanalaridan olamiz
      this.clientProductsStartDate = this.statsStartDate;
      this.clientProductsEndDate = this.statsEndDate;
      this.showClientProductsModal = true;
      await this.fetchClientProducts();
    },
    async fetchClientProducts() {
      if (!this.clientProductsData.clientId) return;
      
      this.clientProductsLoading = true;
      try {
        let url = this.$store.state.hostname + '/TegirmonOrder/getClientProductsStatistics';
        const params = [`clientId=${this.clientProductsData.clientId}`];
        
        if (this.clientProductsStartDate) {
          params.push(`startDate=${this.clientProductsStartDate}`);
        }
        if (this.clientProductsEndDate) {
          params.push(`endDate=${this.clientProductsEndDate}`);
        }
        
        if (params.length > 0) {
          url += '?' + params.join('&');
        }
        
        const response = await axios.get(url);
        if (response.data) {
          this.clientProductsData = response.data;
        } else {
          this.clientProductsData.products = [];
        }
      } catch (error) {
        console.error('Error fetching client products:', error);
        this.$toast.error('Mahsulotlarni yuklashda xatolik yuz berdi');
        this.clientProductsData.products = [];
      } finally {
        this.clientProductsLoading = false;
      }
    },
    async openProductClientsModal(product) {
      this.productClientsData.productId = product.productId;
      this.productClientsData.productName = product.productName;
      // Sana filtrlari - statistika sanalaridan olamiz
      this.productClientsStartDate = this.statsStartDate;
      this.productClientsEndDate = this.statsEndDate;
      this.showProductClientsModal = true;
      await this.fetchProductClients();
    },
    async fetchProductClients() {
      if (!this.productClientsData.productId) return;
      
      this.productClientsLoading = true;
      try {
        let url = this.$store.state.hostname + '/TegirmonOrder/getProductClientsStatistics';
        const params = [`productId=${this.productClientsData.productId}`];
        
        if (this.productClientsStartDate) {
          params.push(`startDate=${this.productClientsStartDate}`);
        }
        if (this.productClientsEndDate) {
          params.push(`endDate=${this.productClientsEndDate}`);
        }
        
        if (params.length > 0) {
          url += '?' + params.join('&');
        }
        
        const response = await axios.get(url);
        if (response.data) {
          this.productClientsData = response.data;
        } else {
          this.productClientsData.clients = [];
        }
      } catch (error) {
        console.error('Error fetching product clients:', error);
        this.$toast.error('Clientlarni yuklashda xatolik yuz berdi');
        this.productClientsData.clients = [];
      } finally {
        this.productClientsLoading = false;
      }
    }
  }
};
</script>

<style scoped>
.equal-height {
  display: flex;
  align-items: stretch;
}

.balance {
  position: relative;
  color: white;
  border-radius: 5px;
  box-shadow: rgba(0, 0, 0, 0.15) 0px 2px 8px,
              rgba(0, 0, 0, 0.17) 0px 12px 13px,
              rgba(0, 0, 0, 0.09) 0px -3px 5px;
}

.main_kassa_bg {
  background-image: linear-gradient(90deg, rgba(255,157,129,1) 24.3%, rgb(255, 110, 78) 78.3%);
}
.main_kassa_mah_qty{
  background-image: linear-gradient(90deg, rgb(65, 203, 221) 24.3%, rgb(64, 170, 241) 78.3%);
}
.main_kassa_client{
  background-image: linear-gradient(90deg, rgb(251, 129, 255) 24.3%, rgb(220, 78, 255) 78.3%);
}
.main_kassa_poluchit {
  background-image: linear-gradient(90deg, rgb(117, 207, 165) 24.3%, rgb(93, 236, 177) 78.3%);
}
.main_kassa_products {
  background-image: linear-gradient(to top, #a3bded 0%, #6991c7 100%);
}
.info_client_header {
  background-image: radial-gradient(circle farthest-corner at 10% 20%, rgb(200, 220, 230) 0%, rgb(196, 223, 228) 90.1%);
}

.hover-row:hover {
  background-color: #e3f2fd !important;
  cursor: pointer;
}

.myTableuserSalaryList {
  table-layout: fixed;
  width: 100%;
  overflow: hidden;
  overflow-y: scroll;
  font-size: 16px;
  max-height: 80px;
  overflow-x: auto;
}

.myTableuserSalaryList th {
  font-weight: 600;
  font-size: 11px;
}

/* Pagination aylana dizayn */
.pagination-circle-btn {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  border: 1.5px solid #64B0FB;
  background: white;
  color: #64B0FB;
  font-size: 12px;
  font-weight: 600;
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 4px;
  cursor: pointer;
  transition: all 0.2s ease;
  padding: 0;
  outline: none;
}

.pagination-circle-btn:hover {
  background: #e3f2fd;
  transform: scale(1.05);
}

.pagination-btn-active {
  background: #64B0FB !important;
  color: white !important;
  border-color: #64B0FB !important;
  box-shadow: 0 2px 6px rgba(100, 176, 251, 0.3);
}

.pagination-btn {
  background: white;
  color: #64B0FB;
}

.myTableuserSalaryList td {
  font-size: 11.5px;
}

.myTableuserSalaryList td {
  text-align: left;
  padding: 3px 10px;
}

.myTableuserSalaryList th {
  text-align: left;
  padding: 5px 10px;
}

.myTableuserSalaryList tr {
  border-bottom: 1px solid rgb(219, 219, 219);
}

.myTableuserSalaryList tr:hover {
  background: #afd1fd;
}

.order_link {
  background-color: #e9ecef;
  color: #495057;
  transition: all 0.3s ease;
}

.order_link:hover {
  background-color: #dee2e6;
}

.bg-active_link {
  background-image: radial-gradient(circle 502px at 2.7% 23.7%, rgb(172, 73, 253) 0%, rgba(33,250,214,1) 99.6%) !important;
  color: white !important;
}
</style>
