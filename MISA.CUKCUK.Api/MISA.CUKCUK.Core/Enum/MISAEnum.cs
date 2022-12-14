using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Enum
{
    /// <summary>
    /// Thiết bị sử dụng
    /// </summary>
    /// Created by: linhpv (08/08/2022)
    public enum Device
    {
        /// <summary>
        /// Không sử dụng
        /// </summary>
        No = 0,

        /// <summary>
        /// Máy tính bảng
        /// </summary>
        Tablet = 1,

        /// <summary>
        /// Máy in
        /// </summary>
        Printer = 2,
    }

    /// <summary>
    /// Kiểu dữ liệu đầu vào
    /// </summary>
    /// Created by: linhpv (11/08/2022)
    public enum InputType
    {
        /// <summary>
        /// Kiểu chữ 
        /// </summary>
        Text = 0,

        /// <summary>
        /// Kiểu số
        /// </summary>
        Number = 1,

        /// <summary>
        /// Kiểm đúng sai
        /// </summary>
        Boolean = 2,
    }

    /// <summary>
    /// Kiểu lọc
    /// </summary>
    /// Created by: linhpv (11/08/2022)
    public enum FilterType
    {
        /// <summary>
        /// Chứa
        /// </summary>
        Contain = 0,

        /// <summary>
        /// Bằng
        /// </summary>
        Equal = 1,

        /// <summary>
        /// Bắt đầu bằng
        /// </summary>
        StartWith = 2,

        /// <summary>
        /// Kết thúc bằng
        /// </summary>
        EndWith = 3,

        /// <summary>
        /// Không chứa
        /// </summary>
        NotContain = 4,

        /// <summary>
        /// Nhỏ hơn
        /// </summary>
        Less = 5,

        /// <summary>
        /// Nhỏ hơn hoặc bằng
        /// </summary>
        LessOrEqual = 6,

        /// <summary>
        /// Lớn hơn
        /// </summary>
        Greater = 7,

        /// <summary>
        /// Lớn hơn hoặc bằng
        /// </summary>
        GreaterOrEqual = 8,

        /// <summary>
        /// Đúng
        /// </summary>
        True = 9,

        /// <summary>
        /// Sai
        /// </summary>
        False = 10,
    }

    /// <summary>
    /// Error code
    /// </summary>
    /// Created by: linhpv (15/08/2022)
    public enum ErrorCode
    {
        /// <summary>
        /// Không có error
        /// </summary>
        NoError = 0,

        /// <summary>
        /// Tên để trống
        /// </summary>
        EmptyName = 1,

        /// <summary>
        /// Mã để trống
        /// </summary>
        EmptyCode = 2,

        /// <summary>
        /// Đơn vị tính trông
        /// </summary>
        EmptyUnit = 3,

        /// <summary>
        /// Giá bán để trống
        /// </summary>
        EmptyPrice = 4,

        /// <summary>
        /// Trùng tên
        /// </summary>
        DuplicateName = 5,
        
        /// <summary>
        /// Trùng mã
        /// </summary>
        DuplicateCode = 6,

        /// <summary>
        /// Thêm mới thất bại
        /// </summary>
        AddFailed = 7,

        /// <summary>
        /// Sửa thất bại
        /// </summary>
        EditFailed = 8,

        /// <summary>
        /// Quá dung lượng cho phép
        /// </summary>
        OverSize = 9,

        /// <summary>
        /// Lỗi server
        /// </summary>
        ServerInternal = 10,

        /// <summary>
        /// Trùng đơn vị tính
        /// </summary>
        DuplicateUnit = 11,
    }

    /// <summary>
    /// Đã định lượng nguyên vật liệu
    /// </summary>
    /// Created by: linhpv (17/08/2022)
    public enum MaterialQuantified
    {
        /// <summary>
        /// Đã định lượng
        /// </summary>
        Quantified = 1,

        /// <summary>
        /// Chưa định lượng
        /// </summary>
        NotQuantified = 0,
    }
}
