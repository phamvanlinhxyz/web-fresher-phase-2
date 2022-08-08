<template>
  <table class="table">
    <thead class="table-header">
      <tr>
        <td style="min-width: 160px">
          <div class="table-column">Loại món</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 180px">
          <div class="table-column">Mã món</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Tên món</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Nhóm thực đơn</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 90px">
          <div class="table-column">Đơn vị tính</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 120px">
          <div class="table-column">Giá bán</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Thay đổi theo giá</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Điều chỉnh tự do</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 140px">
          <div class="table-column">Định lượng nguyên vật liệu</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Hiển thị trên thực đơn</div>
          <div class="table-filter"></div>
        </td>
        <td style="min-width: 90px">
          <div class="table-column">Ngừng bán</div>
          <div class="table-filter"></div>
        </td>
      </tr>
    </thead>
    <tbody class="table-body">
      <tr>
        <td>Alfreds Futterkiste</td>
        <td>Maria Anders</td>
        <td>Germany</td>
      </tr>
      <tr>
        <td>Centro comercial Moctezuma</td>
        <td>Francisco Chang</td>
        <td>Mexico</td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {};
</script>

<style>
@import url(@/css/view/menu/menutable.css);
</style>