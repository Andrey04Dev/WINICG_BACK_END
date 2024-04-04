using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace webapi.Interfaces.Files
{
    public class FileRepository : IFileRepository
    {
        private readonly DbConnection db;

        public FileRepository(DbConnection db   )
        {
            this.db = db;
        }
        public async Task<List<FILES>> AddFiles([FromForm] List<FILES> files, string id)
        {
            if (files == null) return null;
            using var conn = db.GetConnection();
            conn.Open();
            List<FILES> adddFile = new List<FILES>();

            foreach (FILES file in files)
            {
                var result = await conn.QueryAsync<FILES>("AUD.SP_ADD_FILES",
                    new { 
                        @IDMODULE =  id,
                        @FILENAME =  file.FILENAME,
                        @EXTENSION =  file.EXTENSION,
                        @BINARY_FILE = file.BINARY_FILE,
                    }, 
                    commandType: System.Data.CommandType.StoredProcedure);

                var resultFile = MappingFiles(result);
                adddFile.Add(resultFile);
            }
            conn.Close();
            conn.Dispose();
            return adddFile;
        }

        public async Task<int> GetCountFiles()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM AUD.FILES";
            var GetAudit = await conn.QueryAsync<int>(sql);
            conn.Close();
            conn.Dispose();
            var result = 0;
            foreach (var audit in GetAudit)
            {
                result = audit;
            }
            return result;
        }

        public async Task<IEnumerable<FILES>> ListFilesByID(string id)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var ListaFiles  =  await conn.QueryAsync<FILES>("AUD.SP_GET_FILES_BY_ID",
                    new
                    {
                        @IDMODULE = id,
                    },
                    commandType: System.Data.CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose();

            return ListaFiles;
        }

        public async Task<FILES> removeImage(string idModule, string idFile)
        {
            using var conn = db.GetConnection();
            conn.Open();
            var FileRemoved = await conn.QueryAsync<FILES>("AUD.SP_DELETE_FILE",
            new
            {
                    @IDMODULE = idModule,
                    @IDFILE = idFile
                },
                commandType: CommandType.StoredProcedure);
            conn.Close();
            conn.Dispose();
            var resultFile = MappingFiles(FileRemoved);

            return resultFile;
        }

        private FILES MappingFiles(IEnumerable<FILES> files)
        {
            FILES fileResult = new FILES();
            foreach (FILES file in files)
            {
                fileResult.IDFILE = file.IDFILE;
                fileResult.FILENAME = file.FILENAME;
                fileResult.EXTENSION = file.EXTENSION;
                fileResult.IDMODULE = file.IDMODULE;
                fileResult.CREATEDATE = file.CREATEDATE;
                fileResult.UPDATEDATE = file.UPDATEDATE;
            }
            return fileResult;
        }
    }
}
