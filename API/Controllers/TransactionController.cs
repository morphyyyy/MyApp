using API.Entities;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> GetTransactions()
        {
            try
            {
                var transactions = await _transactionRepository.GetAllTransactions();

                if (transactions == null)
                { 
                    return NotFound();
                }

                var transactionDTOs = new List<TransactionDTO>();

                foreach (var transaction in transactions)
                {
                    TransactionDTO transactionDTO = new TransactionDTO
                    {
                        Id = transaction.Id,
                        TypeId = transaction.TypeId,
                        Date = transaction.Date,
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        CreatedDate = transaction.CreatedDate,
                        UpdatedDate = transaction.UpdatedDate,
                        DeletedDate = transaction.DeletedDate
                    };

                    transactionDTOs.Add(transactionDTO);
                }



                return Ok(transactionDTOs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
