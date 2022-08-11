<template>
  <div class="menu-group-bg">
    <div class="menu-group-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm nhóm thực đơn</div>
          <div @click="toggleMenuGroupPopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Mã nhóm <span style="color: red">(*)</span>
              </div>
              <base-input />
            </div>
            <div class="form-input">
              <div class="form-label">
                Tên nhóm <span style="color: red">(*)</span>
              </div>
              <base-input />
            </div>
            <div class="form-input">
              <div class="form-label">
                Thuộc loại <span style="color: red">(*)</span>
              </div>
              <base-input :disabled="true" />
            </div>
            <div class="form-input">
              <div class="form-label">Chế biến tại</div>
              <base-combobox :listItem="kitchens" tableName="Kitchen" />
            </div>
            <div class="form-input">
              <div class="form-label">Diễn giải</div>
              <textarea class="textarea" rows="3"></textarea>
            </div>
          </div>
        </div>
        <div class="mgf-footer">
          <div class="pdf-left">
            <base-button content="Giúp" icon="question-circle" />
          </div>
          <div class="pdf-right">
            <base-button content="Cất" icon="save" />
            <base-button content="Hủy" icon="cancel" />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapActions, mapState } from "vuex";

export default {
  computed: mapState({
    kitchens: (state) => state.dish.kitchens,
  }),
  methods: {
    ...mapActions(["loadAllKitchen", "toggleMenuGroupPopup"]),
  },
  created() {
    this.loadAllKitchen();
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menugroupform.css);
</style>