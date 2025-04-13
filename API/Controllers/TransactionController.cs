using API.Entities;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<TransactionDTO>>> List()
        {
            try
            {
                var transactions = await _transactionRepository.List();

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
                        Date = transaction.Date,
                        Description = transaction.Description,
                        Amount = transaction.Amount,
                        Balance = transaction.Balance,
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

        [HttpPost("Create")]
        public async Task<ActionResult<TransactionDTO>> Create(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction
                {
                    Date = transactionDTO.Date,
                    Description = transactionDTO.Description,
                    Amount = transactionDTO.Amount,
                    Balance = transactionDTO.Balance,
                    CreatedDate = DateTime.UtcNow,
                };

                transaction = await _transactionRepository.Create(transaction);

                transactionDTO.Id = transaction.Id;
                transactionDTO.Date = transaction.Date;
                transactionDTO.Description = transaction.Description;
                transactionDTO.Amount = transaction.Amount;
                transactionDTO.Balance = transaction.Balance;
                transactionDTO.CreatedDate = transaction.CreatedDate;
                transactionDTO.UpdatedDate = transaction.UpdatedDate;
                transactionDTO.DeletedDate = transaction.DeletedDate;

                return Ok(transactionDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<TransactionDTO>> Update(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = new Transaction
                {
                    Id = transactionDTO.Id,
                    Date = transactionDTO.Date,
                    Description = transactionDTO.Description,
                    Amount = transactionDTO.Amount,
                    Balance = transactionDTO.Balance,
                    UpdatedDate = DateTime.Now
                };

                transaction = await _transactionRepository.Update(transaction);

                transactionDTO.Id = transaction.Id;
                transactionDTO.Date = transaction.Date;
                transactionDTO.Description = transaction.Description;
                transactionDTO.Amount = transaction.Amount;
                transactionDTO.Balance = transaction.Balance;
                transactionDTO.CreatedDate = transaction.CreatedDate;
                transactionDTO.UpdatedDate = transaction.UpdatedDate;
                transactionDTO.DeletedDate = transaction.DeletedDate;

                return Ok(transactionDTO);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<int> Delete(int id)
        {
            return await _transactionRepository.Delete(id);
        }
    }
}
