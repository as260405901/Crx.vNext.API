using System.ComponentModel.DataAnnotations;

namespace Crx.vNext.Model.InputModel
{
    public class InputModel<T>
    {
        /// <summary>
        /// 参数
        /// </summary>
        [Required(ErrorMessage = "参数不能为空！")]
        public T Data { get; set; }
    }
}
