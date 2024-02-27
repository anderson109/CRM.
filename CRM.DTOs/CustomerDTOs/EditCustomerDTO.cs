using System.ComponentModel.DataAnnotations;
using System;
namespace CRM.DTOs.CustomerDTOs
{
    public class EditCustomerDTO
    {

        public EditCustomerDTO(GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            Id = getIdResultCustomerDTO.Id;
            Name = getIdResultCustomerDTO.Name;
            LastName = getIdResultCustomerDTO.LastName;
            Address = getIdResultCustomerDTO.Address;
        }
        public EditCustomerDTO()
        {
            Name = string.Empty;
            LastName = string.Empty;
        }
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }
        public string Name { get; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Nombre no puede tener mas de 50 caracteres.")]
        public string LastName { get; set; }

        [Display(Name = "Direccion")]
        [MaxLength(255, ErrorMessage = "El campo Direccion no puede tener mas de 255 caracteres.")]
        public string? Address { get; set; }
    }   
    
}
