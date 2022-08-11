<template>
  <div class="unit-bg">
    <div class="unit-form">
      <div class="mgf-inner">
        <div class="popup-dish-header">
          <div>Thêm đơn vị tính</div>
          <div @click="toggleUnitPopup">
            <base-icon iconName="x-close" />
          </div>
        </div>
        <div class="mgf-body">
          <div class="mgf-input">
            <div class="form-input">
              <div class="form-label">
                Đơn vị tính <span style="color: red">(*)</span>
              </div>
              <base-input />
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
import { mapActions } from "vuex";

export default {
  methods: {
    ...mapActions(["toggleUnitPopup"]),
  },
};
</script>

<style scoped>
@import url(@/css/view/menu/menugroupform.css);
</style>