const VI_Required_Error = "Trường này không được để trống.";
const VI_Data_Changed = "Dữ liệu đã thay đổi, bạn có muốn cất không?";

const VI_Error_Msg = {
  required: "Trường này không được để trống.",
  server: "Đã có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.",
  capacity:
    "Ảnh không tải được do vượt quá dung lượng. Vui lòng chọn ảnh có dung lượng nhỏ hơn 5 MB.",
};

const VI_Confirm_Delete = (dishCode, dishName) => {
  return `Bạn có chắc chắn muốn xóa món <${dishCode} - ${dishName}> không?`;
};

const VI_Menu_Item = {
  dashboard: "Tổng quan",
  menu: "Thực đơn",
};

const VI_Toolbar = {
  add: "Thêm",
  copy: "Nhân bản",
  edit: "Sửa",
  delete: "Xóa",
  reload: "Nạp",
};

const VI_Table_Column = {
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

const VI_Pagination = {
  page: "Trang",
  of: "trên",
  noData: "Không có dữ liệu",
  record(display, totalRc) {
    return `Hiển thị ${display} trên ${totalRc} kết quả`;
  },
};

const VI_Filter_Type = {
  Contain: "∗ : Chứa", // Chứa
  Equal: "= : Bằng", // Bằng
  StartWith: "+ : Bắt đầu bằng", // Bắt đầu bằng
  EndWith: "- : Kết thúc bằng", // Kết thúc bằng
  NotContain: "! : Không chứa", // Không chứa
  Less: "< : Nhỏ hơn", // Nhỏ hơn
  LessOrEqual: "≤ : Nhỏ hơn hoặc bằng", // Nhỏ hơn hoặc bằng
  Greater: "> : Lớn hơn", // Lớn hơn
  GreaterOrEqual: "≥ : Lớn hơn hoặc bằng", // Lớn hơn hoặc bằng
};

const EN_Required_Error = "This field must not be empty.";
const EN_Data_Changed = "Data were changed, do you want to save?";
const EN_Error_Msg = "An error has occurred, please contact MISA for support.";

const EN_Confirm_Delete = (dishCode, dishName) => {
  return `Are you sure you want to delete item <${dishCode} - ${dishName}>?`;
};

const EN_Menu_Item = {
  dashboard: "General report",
  menu: "Menu",
};

const EN_Toolbar = {
  add: "Add",
  copy: "Copy",
  edit: "Edit",
  delete: "Delete",
  reload: "Reload",
};

const EN_Table_Column = {
  dishType: "Item type",
  dishCode: "Item ID",
  dishName: "Item name",
  menuGroup: "Menu category",
  unit: "Unit",
  price: "Cost price",
  seasonalPrice: "Seasonal price items",
  flexiblePrice: "Flexible price adjustment",
  materialQuantified: "Ingr. formula",
  showOnMenu: "Display on menu",
  stopSale: "Inactive",
};

const EN_Pagination = {
  page: "Page",
  of: "of",
  noData: "No data",
  record(display, totalRc) {
    return `${display}/${totalRc} results`;
  },
};

export default {
  VI_Required_Error,
  VI_Data_Changed,
  VI_Error_Msg,
  VI_Menu_Item,
  VI_Toolbar,
  VI_Table_Column,
  VI_Pagination,
  VI_Filter_Type,
  VI_Confirm_Delete,
  EN_Required_Error,
  EN_Data_Changed,
  EN_Error_Msg,
  EN_Menu_Item,
  EN_Toolbar,
  EN_Table_Column,
  EN_Pagination,
  EN_Confirm_Delete,
};
