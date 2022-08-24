<template>
  <div class="menu-pagination">
    <div class="menu-pagination-left">
      <div
        class="pagination-icon"
        @click="handleFirstPage"
        :class="{ disabled: pageIndex == 1 || pageNumber == 0 }"
      >
        <base-icon iconName="page-first" />
      </div>
      <div
        class="pagination-icon"
        @click="handlePrevPage"
        :class="{ disabled: pageIndex == 1 || pageNumber == 0 }"
      >
        <base-icon iconName="page-prev" />
      </div>
      <div class="separator-hor"></div>
      <div class="pagination-icon">{{ pagiContent.page }}</div>
      <form @submit="handleSubmitForm">
        <base-input v-model="pageNumber" />
      </form>
      <div class="pagination-icon">{{ pagiContent.of }} {{ totalPage }}</div>
      <div class="separator-hor"></div>
      <div
        class="pagination-icon"
        @click="handleNextPage"
        :class="{ disabled: pageNumber == totalPage }"
      >
        <base-icon iconName="page-next" />
      </div>
      <div
        class="pagination-icon"
        @click="handleLastPage"
        :class="{ disabled: pageNumber == totalPage }"
      >
        <base-icon iconName="page-last" />
      </div>
      <div class="separator-hor"></div>
      <div class="pagination-icon" @click="loadDishsByPaging">
        <base-icon iconName="refresh" />
      </div>
      <div class="separator-hor"></div>
      <base-combobox
        :listItem="listTotalRecord"
        tableName="totalRecord"
        disabled="true"
        value="3"
        @change="selectTotalRecord"
      />
    </div>
    <div class="menu-pagination-right">
      <div v-if="!isLoadingDish && totalRecord != 0">
        {{
          pagiContent.record(
            `${(pageIndex - 1) * pageSize + 1} -  ${
              pageIndex * pageSize < totalRecord
                ? pageIndex * pageSize
                : totalRecord
            }`,
            totalRecord
          )
        }}
      </div>
      <div v-if="isLoadingDish || totalRecord == 0">{{ pagiContent.noData }}</div>
    </div>
  </div>
</template>

<script>
import resources from "@/resources";
import { mapActions, mapState } from "vuex";

export default {
  data() {
    return {
      pageNumber: 1,
      listTotalRecord: [
        {
          totalRecordID: "1",
          totalRecordName: 25,
        },
        {
          totalRecordID: "2",
          totalRecordName: 50,
        },
        {
          totalRecordID: "3",
          totalRecordName: 100,
        },
      ],
      pagiContent: null,
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    pageIndex: (state) => state.dish.pageIndex,
    pageSize: (state) => state.dish.pageSize,
    totalPage: (state) => state.dish.totalPage,
    totalRecord: (state) => state.dish.totalRecord,
    isLoadingDish: (state) => state.dish.isLoadingDish,
  }),
  watch: {
    pageIndex(newVal) {
      this.pageNumber = newVal;
    },
    totalRecord(newVal) {
      if (newVal === 0) {
        this.pageNumber = 0;
      } else {
        this.pageNumber = this.pageIndex;
      }
    },
  },
  methods: {
    ...mapActions(["updatePageSize", "updatePageIndex", "loadDishsByPaging"]),
    /**
     * Xử lý sự kiện chọn số bản ghi trên trang
     * @param {*} item số bản ghi
     * Author: linhpv (10/08/2022)
     */
    selectTotalRecord(item, table) {
      this.pageNumber = 1;
      this.updatePageSize(item.totalRecordName);
      this.loadDishsByPaging();
    },
    /**
     * Xử lý sự kiện chọn trang sau
     * Author: linhpv (10/08/2022)
     */
    handleNextPage() {
      // Nếu trang hiện tại nhỏ hơn trang cuối thì mới chuyển
      if (this.pageIndex < this.totalPage) {
        ++this.pageNumber;
        this.getData();
      }
    },
    /**
     * Xử lý sự kiện chọn trang trước
     * Author: linhpv (10/08/2022)
     */
    handlePrevPage() {
      // Nếu trang hiện tại lớn hơn trang đầu thì mới chuyển
      if (this.pageIndex > 1) {
        --this.pageNumber;
        this.getData();
      }
    },
    /**
     * Xử lý sự kiện chọn trang cuối
     * Author: linhpv (10/08/2022)
     */
    handleLastPage() {
      // Nếu trang hiện tại khác trang cuối thì mới chuyển
      if (this.pageIndex < this.totalPage) {
        this.pageNumber = this.totalPage;
        this.getData();
      }
    },
    /**
     * Xử lý sự kiện chọn trang đầu
     * Author: linhpv (10/08/2022)
     */
    handleFirstPage() {
      // Nếu trang hiện tại khác trang đầu thì mới chuyển
      if (this.pageIndex > 1) {
        this.pageNumber = 1;
        this.getData();
      }
    },
    /**
     * Xử lý sự kiện chọn số trang
     * @param {*} e
     * Author: linhpv (10/08/2022)
     */
    handleSubmitForm(e) {
      e.preventDefault();
      // Xử lý số liệu nhập vào
      if (this.pageNumber <= 0 || !this.pageNumber) {
        this.pageNumber = 1;
      } else if (this.pageNumber > this.totalPage) {
        this.pageNumber = this.totalPage;
      }
      this.getData();
    },
    /**
     * Lấy dữ liệu
     * Author: linhpv (10/08/2022)
     */
    getData() {
      this.updatePageIndex(this.pageNumber);
      this.loadDishsByPaging();
    },
  },
  created() {
    this.pagiContent = resources[`${this.langCode}_Pagination`];
  },
  mounted() {
    this.pageNumber = this.pageIndex;
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menupagination.css);
</style>