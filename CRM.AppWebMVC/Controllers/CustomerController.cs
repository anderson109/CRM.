using Microsoft.AspNetCore.Mvc;
using CRM.DTOs.CustomerDTOs;
using System.Linq.Expressions;

namespace CRM.AppWebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClientCRMAPI;

        //constructor que recibe una instancia de IHttpClientFactory para crear el cliente HTTP
        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientCRMAPI = httpClientFactory.CreateClient("CRMAPI");
        }

        //metodo para mostrar la lista de clientes
        public async Task<IActionResult> Index(SearchQueryCustomerDTO searchQueryCustomerDTO, int CountRow = 0)
        {
            //configuracion de valores por defecto para la busqueda
            if (searchQueryCustomerDTO.SendRowCount == 0)
                searchQueryCustomerDTO.SendRowCount = 2;
            if (searchQueryCustomerDTO.Take == 0)
                searchQueryCustomerDTO.Take = 10;

            var result = new SearchResultCustomerDTO();

            //realizar una solicitud una solicitud http post para buscar cliente en el servicio web
            var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer/search", searchQueryCustomerDTO);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<SearchResultCustomerDTO>();

            result = result != null ? result : new SearchResultCustomerDTO();

            //configuracion de valores para la vista
            if (result.CountRow == 0 && searchQueryCustomerDTO.SendRowCount == 1)
                result.CountRow = CountRow;

            ViewBag.CountRow = result.CountRow;
            searchQueryCustomerDTO.SendRowCount = 0;
            ViewBag.SearchQuery = searchQueryCustomerDTO;

            return View(result);
        }

        // Metodo para mostrar los detalles de un cliente
        public async Task<IActionResult> Details(int id)
        {
            var result = new GetIdResultCustomerDTO();

            // Realizar una solicitud HTTP GET para obtener los detalles del cliente por ID
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        // Metodo para mostrar el formulario de creacion de un cliente
        public ActionResult Create()
        {
            return View();
        }

        // Metodo para procesar la creacion de un cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCustomerDTO createCustomerDTO)
        {
            try
            {
                // Realizar una solicitud http post para crear un nuevo cliente
                var response = await _httpClientCRMAPI.PostAsJsonAsync("/customer", createCustomerDTO);

                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "error al intentar guardar el registro";
                return View();
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        // Metodo para mostrar el formulario de edicion en in cliente 
        public async Task<IActionResult> Edit(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await _httpClientCRMAPI.GetAsync("/customer/" + id);

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomer>();

            return View(new EditCustomerDTO(result ?? new GetIdResultCustomerDTO()));
        }

        // Metodo para procesar la edicion de un cliente 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCustomerDTO editCustomerDTO)
        {
            return await NewMethod(editCustomerDTO);
        }
        private async Task<IActionResult> NewMethod(EditCustomerDTO editCustomerDTO)
        {
            var response await _httpClientCRMAPI.PutAsJsonAsync("/customer/", editCustomerDTO);

            if (response.IsSeccessStatusCode)

            try
            {
                // Realizar una solicitud HTTP Put para editar el cliente 
                var response = await _httpClientCRMAPI.PutAsJsonAsync("/customer/", editCustomerDTO);
                
                if (response.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar editar el registro";
                return View();
            }
            catch (Exception ex)
            
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        //Metodo para mostrar la pagina de confirmacion de eliminacion de un cliente
        public async Task<IActionResult> Delete(int id)
        {
            var result = new GetIdResultCustomerDTO();
            var response = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDto>();

            if (response.IsSuccessStatusCode)
                result = await response.Content.ReadFromJsonAsync<GetIdResultCustomerDTO>();

            return View(result ?? new GetIdResultCustomerDTO());
        }

        // Metodo para procesar la eliminacion de un cliente 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, GetIdResultCustomerDTO getIdResultCustomerDTO)
        {
            try
            {
                // Realizar una solicitud HTTP DELETE para eliminar el cliente ID
                var response = await _httpClientCRMAPI.DeleteAsync("(customer/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Error = "Error al intentar eliminar el registro";
                return View(getIdResultCustomerDTO);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(getIdResultCustomerDTO);
            }
        }
    }
}
