// Thông báo trên các dialog
const VI_Dialog_Msg = {
  dataChanged: "Dữ liệu đã thay đổi, bạn có muốn cất không?",
  confirmDelete: (dishCode, dishName) => {
    return `Bạn có chắc chắn muốn xóa món <${dishCode} - ${dishName}> không?`;
  },
};

// Chữ hiển thị trên menu
const VI_Menu_Item = {
  dashboard: "Tổng quan",
  menu: "Thực đơn",
};

// Chữ hiển thị trên thanh toolbar
const VI_Toolbar = {
  add: "Thêm",
  copy: "Nhân bản",
  edit: "Sửa",
  delete: "Xóa",
  reload: "Nạp",
};

// Chữ hiển thị trên table
const VI_Table = {
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
  dish: "Món ăn",
  quantified: "Đã thiết lập",
  notQuantified: "Chưa thiết lập",
  yes: "Có",
  no: "Không",
};

// Chữ hiển thị trên phân trang
const VI_Pagination = {
  page: "Trang",
  of: "trên",
  noData: "Không có dữ liệu",
  record(display, totalRc) {
    return `Hiển thị ${display} trên ${totalRc} kết quả`;
  },
};

// Kiểu lọc dữ liệu
const VI_Filter_Type = {
  contain: "∗ : Chứa",
  equal: "= : Bằng",
  startWith: "+ : Bắt đầu bằng",
  endWith: "- : Kết thúc bằng",
  notContain: "! : Không chứa",
  less: "< : Nhỏ hơn",
  lessOrEqual: "≤ : Nhỏ hơn hoặc bằng",
  greater: "> : Lớn hơn", // Lớn hơn
  greaterOrEqual: "≥ : Lớn hơn hoặc bằng",
};

// Thông báo lỗi
const VI_Error_Msg = {
  required: "Trường này không được để trống.",
  server: "Đã có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.",
  capacity:
    "Ảnh không tải được do vượt quá dung lượng. Vui lòng chọn ảnh có dung lượng nhỏ hơn 5 MB.",
  noError: "",
  emptyName: "Tên không được để trống.",
  emptyCode: "Mã không được để trống.",
  emptyUnit: "Đơn vị tính không phép để trống.",
  emptyPrice: "Giá bán không phép để trống.",
  duplicateName: (name) => {
    return `Tên <${name}> đã tồn tại trong hệ thống.`;
  },
  duplicateCode: (code) => {
    return `Mã <${code}> đã tồn tại trong hệ thống.`;
  },
  duplicateUnit: (unit) => {
    return `Đơn vị tính <${unit}> đã tồn tại trong hệ thống.`;
  },
  addFailed: "Thêm mới thất bại, vui lòng thử lại.",
  editFailed: "Sửa thất bại, vui lòng thử lại.",
  overSize:
    "Ảnh không tải được do vượt quá dung lượng. Vui lòng chọn ảnh có dung lượng nhỏ hơn 5 MB.",
  serverInternal: "Đã có lỗi xảy ra. Vui lòng liên hệ MISA để được hỗ trợ.",
  duplicateMaterial:
    "Danh sách nguyên vật liệu đã bị trùng. Vui lòng kiểm tra lại.",
};

export default {
  VI_Error_Msg,
  VI_Menu_Item,
  VI_Toolbar,
  VI_Table,
  VI_Pagination,
  VI_Filter_Type,
  VI_Dialog_Msg,
};
