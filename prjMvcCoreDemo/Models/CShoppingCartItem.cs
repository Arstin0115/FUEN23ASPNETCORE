using System.ComponentModel;

namespace prjMvcCoreDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        [DisplayName("採購量")]
        public int count { get; set; }
        [DisplayName("金額")]
        public decimal price { get; set; }
        public decimal 小計
        {
            get
            {
                return this.price * this.count;
            }
        }

        public TProduct product { get; set; }
    }
}
