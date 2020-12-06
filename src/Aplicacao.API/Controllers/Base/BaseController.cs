using Aplicacao.Application.DTOs;
using Aplicacao.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Aplicacao.API.Controllers.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Tid"></typeparam>
    public abstract class BaseController<T, Tid> : ControllerBase
        where T : BaseDTOEntity<Tid>
    {
        private readonly Stopwatch sw;
        /// <summary>
        /// 
        /// </summary>
        protected readonly IAppService<T, Tid> _appService;

        ///<Summary>
        /// Constructor BaseGetController
        ///</Summary>
        protected BaseController(IAppService<T, Tid> appService)
        {
            _appService = appService;

            sw = new Stopwatch();
        }

        /// <summary>
        /// Retorna a lista do objeto com os parâmetros colunaOrdenacao, direcao, qtd and pule        
        /// </summary>
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize("Bearer")]
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> GetAll()
        {
            sw.Start();

            var retorno = await _appService.GetAll();

            sw.Stop();

            return retorno.Any()
                ? Ok(new
                {
                    success = true,
                    data = retorno,
                    tempoProcessamento = TempoProcessamento(sw)
                })
                : (IActionResult)NoContent();
        }

        /// <summary>
        /// Retorna o objeto  by id
        /// </summary>
        /// <param name="id">Identificador do objeto</param>
        //[Authorize("Bearer")]
        [HttpGet("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Get(Tid id)
        {
            sw.Start();

            var retorno = await _appService.Get(id);

            sw.Stop();

            return retorno != null
                ? Ok(new
                {
                    success = true,
                    data = retorno,
                    tempoProcessamento = TempoProcessamento(sw)
                })
                : (IActionResult)NoContent();
        }

        ///<summary>
        /// Criação do objeto
        ///</summary>
        ///<param name='t'>Objeto</param>
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize("Bearer")]
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public virtual async Task<IActionResult> Post([FromBody] T t, ApiVersion apiVersion)
        {
            if (ModelState.IsValid)
            {
                sw.Start();

                var retorno = await _appService.Add(t);

                sw.Stop();

                if (retorno != null && !retorno.ValidationResult.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        data = retorno.ValidationResult.ToString(),
                        tempoProcessamento = TempoProcessamento(sw)
                    });

                //return CreatedAtAction(nameof(Get), new { apiVersion = apiVersion.ToString(), id = retorno.Identificador }, retorno);

                return retorno != null
                    ? CreatedAtAction(
                        nameof(Get),
                        new { apiVersion = apiVersion.ToString(), 
                            id = retorno.Identificador },
                        new
                        {
                            success = true,
                            data = retorno,
                            tempoProcessamento = TempoProcessamento(sw)
                        })
                    : (IActionResult)NoContent();
            }

            return BadRequest(t);
        }

        ///<summary>
        ///Atualização do objeto
        ///</summary>
        ///<param name='t'>Objeto </param>
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize("Bearer")]
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Put([FromBody] T t)
        {
            if (ModelState.IsValid)
            {
                sw.Start();

                var retorno = await _appService.Update(t);

                sw.Stop();

                if (retorno != null && !retorno.ValidationResult.IsValid)
                    return BadRequest(new
                    {
                        success = false,
                        data = retorno.ValidationResult.ToString(),
                        tempoProcessamento = TempoProcessamento(sw)
                    });

                return Ok(new
                {
                    success = true,
                    data = retorno,
                    tempoProcessamento = TempoProcessamento(sw)
                });

            }
            return BadRequest(t);
        }

        /// <summary>
        /// Para deletar um objeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //[ApiExplorerSettings(IgnoreApi = true)]
        //[Authorize("Bearer")]
        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public virtual async Task<IActionResult> Delete(Tid id)
        {
            if (ModelState.IsValid)
            {
                sw.Start();

                var retorno = await _appService.Remover(id);

                sw.Stop();

                return Ok(new
                {
                    success = true,
                    data = retorno,
                    tempoProcessamento = TempoProcessamento(sw)

                }); ;
            }
            return BadRequest(id);
        }

        private string TempoProcessamento(Stopwatch stopwatch)
        {
            return $"{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Seconds} segundos e " +
                        $"{TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).Milliseconds} milessegundos";
        }
    }
}