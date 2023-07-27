using Web2T.Models;
using System.ComponentModel.DataAnnotations;

namespace Web2T.ModelViews
{
    public class ChangeAddressViewModel
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Địa chỉ hiện tại")]
        public string AddressNow { get; set; }

        [Display(Name = "Địa chỉ mới")]
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ mới")]
        [MinLength(5, ErrorMessage = "Bạn cần thêm địa chỉ mới tối thiểu 5 ký tự")]
        public string Address { get; set; }

    }
}