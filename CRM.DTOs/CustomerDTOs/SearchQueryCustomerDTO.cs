using System.ComponentModel.DataAnnotations;

namespace CRM.DTOs.CustomerDTOs
{
    public class SearchQueryCustomerDTO
    {
        public readonly int CountRow;

        [Display(Name = "Nombre")]
        public string? Name_Like { get; set; }
        [Display(Name = "Apellido")]
        public string? LastName_Like { get; set; }
        [Display(Name = "Pagina")]
        public int Skip { get; set; }
        [Display(Name = "CantReg X Pagina")]
        public int Take { get; set; }
        /// <summary>
        /// 1 = No se cuenta los resultados de la busqueda
        /// 2= Cuenta los resultados de la bsuqueda
        /// </summary>
        public byte SendRowCount { get; set; }
    }
}
