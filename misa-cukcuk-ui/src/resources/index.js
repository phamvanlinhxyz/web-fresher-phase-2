const VN_Required_Error = "Trường này không được để trống.";
const VN_Data_Changed = "Dữ liệu đã thay đổi, bạn có muốn cất không?";
const VN_Error_Msg = "Đã có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.";

const VN_Confirm_Delete = (dishCode, dishName) => {
  return `Bạn có chắc chắn muốn xóa món <${dishCode} - ${dishName}> không?`;
};

const VN_Menu_Item = {
  dashboard: "Tổng quan",
  menu: "Thực đơn",
};

const VN_Toolbar = {
  add: "Thêm",
  copy: "Nhân bản",
  edit: "Sửa",
  delete: "Xóa",
  reload: "Nạp",
};

const VN_Table_Column = {
  dishType: "Loại món",
  dishCode: "Mã món",
  dishName: "Tên món",
  menuGroup: "Nhóm thực đơn",
  unit: "Đơn vị tính",
  price: "Giá bán",
  seasonalPrice: "Thay đổi theo thời giá",
  flexiblePrice: "Điều chỉnh tự do",
  materialQuantified: "Định lượng NVL",
  showOnMenu: "Hiển thị trên thực đơn",
  stopSale: "Ngừng bán",
};

const VN_Pagination = {
  page: "Trang",
  of: "trên",
  noData: "Không có dữ liệu",
  record(display, totalRc) {
    return `Hiển thị ${display} trên ${totalRc} kết quả`;
  },
};

const EN_Menu_Item = {
  dashboard: "General report",
  menu: "Menu",
};

export default {
  VN_Required_Error,
  VN_Data_Changed,
  VN_Error_Msg,
  VN_Menu_Item,
  VN_Toolbar,
  VN_Table_Column,
  VN_Pagination,
  VN_Confirm_Delete,
  EN_Menu_Item,
};
