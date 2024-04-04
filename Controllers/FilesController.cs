using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.DTO.Files;
using webapi.Interfaces.Files;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository fileRepository;
        private readonly IMapper mapper;

        public FilesController(IFileRepository fileRepository, IMapper mapper)
        {
            this.fileRepository = fileRepository;
            this.mapper = mapper;
        }
        [HttpGet("getCountFiles")]
        public async Task<IActionResult> GetCountFiles()
        {
            try
            {
                var listfiles = await fileRepository.GetCountFiles();
                if (listfiles == null) BadRequest("La lista de archivos esta vacia!");
                return Ok(listfiles);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getFilesById/{id}")]
        public async Task<IActionResult>GetFilesByID(string id)
        {
            try
            {
                if (id == null) BadRequest($"The id is: {string.Empty}");
                var listfiles =  await fileRepository.ListFilesByID(id);
                if (listfiles == null) BadRequest("La lista de archivos esta vacia!");
                return Ok(listfiles);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addFiles")]
        public async Task<IActionResult> AddFiles([FromForm] AddFilesDTO addFilesDTO)
        {
            if (addFilesDTO == null) { return null; }
            var dtoFile = new List<FILES>();

            foreach (var item in addFilesDTO.files)
            {
                    FILES fileSave = new FILES();
                    byte[] fileBytes = await SaveFile(item);

                    fileSave.FILENAME = Path.GetFileNameWithoutExtension(item.FileName);
                    fileSave.BINARY_FILE = fileBytes;
                    fileSave.EXTENSION = Path.GetExtension(item.FileName).Substring(1);
                    fileSave.IDMODULE = addFilesDTO.id;

                    dtoFile.Add(fileSave);

                    fileSave = null;
                
            }

            var fileAdded = await fileRepository.AddFiles(dtoFile,addFilesDTO.id);
            if (fileAdded == null) BadRequest("Ocurrio un error");
            return Ok(fileAdded);

        }

        [HttpDelete("deleteFile/{idModule}/{idFile}")]
        public async Task<IActionResult> DeleteFiles(string idModule, string idFile)
        {
            if (idModule == null || idFile == null) BadRequest("Los ID's estan vacios");
            var deleteFile =  await fileRepository.removeImage(idModule, idFile);   

            var filename =  deleteFile.FILENAME + deleteFile.EXTENSION;

            return Ok($"El archivo con el nombre ${filename} fue eliminado");
        }


        private async Task<byte[]> SaveFile(IFormFile formFile)
        {
            if (formFile == null)
            {
                return null;
            }
            using var stream = new MemoryStream();
            await formFile.CopyToAsync(stream);
            var fileBytes = stream.ToArray();
            return fileBytes;
            //return await storageRepository.CreateStorage(fileBytes, formFile.ContentType, Path.GetExtension(formFile.FileName), ConstantApp.FileContainer, Guid.NewGuid().ToString());
        }
    }
}
