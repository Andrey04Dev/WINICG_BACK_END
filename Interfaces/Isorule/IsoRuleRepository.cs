﻿using Dapper;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using webapi.Data;
using webapi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace webapi.Interfaces.Isorule
{
    public class IsoRuleRepository : IIsoRuleRepository
    {
        private readonly DbConnection db;

        public IsoRuleRepository(DbConnection db)
        {
            this.db = db;
        }
        public async Task<ISORULE> AddIsoRule(ISORULE rule)
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var addRule = await conn.QueryAsync<ISORULE>("ISO.SP_ADD_ISORULE",
                    new
                    {
                        @IDAUDIT = rule.IDAUDIT == "" ?  null :  rule.IDAUDIT,
                        @IDCERTIFICATION = rule.IDCERTIFICATION,
                        @NAMERULE = rule.NAMERULE,
                        @RULE_DESCRIPTION = rule.RULE_DESCRIPTION
                    }, 
                    commandType: CommandType.StoredProcedure );
                conn.Close();
                conn.Dispose();
                var result = MappingIsorule(addRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<List<ISORULE>> GetAllIsoRule()
        {
            try
            {
                using var conn =  db.GetConnection();
                conn.Open();
                var getAllIsorule = await conn.QueryAsync<ISORULE,CERTIFICATION,AUDITS,ISORULE>("ISO.SP_GET_ALL_ISORULE",
                   map: (isorule, certification, audits) => { isorule.CERTIFICATION = certification; isorule.AUDITS = audits; return isorule; },
                    splitOn: "IDCERTIFICATION, IDAUDIT",
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                return (List<ISORULE>)getAllIsorule;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<int> GetCountIsoRule()
        {
            using var conn = db.GetConnection();
            conn.Open();
            var sql = "SELECT COUNT(*) FROM ISO.ISORULE";
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

        public async Task<ISORULE> GetIsoRuleById(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var getIsoruleById = await conn.QueryAsync<ISORULE, CERTIFICATION, AUDITS, ISORULE>("ISO.SP_GET_ISORULE_BY_ID",
                    map: (isorule, certification, audits) => { isorule.CERTIFICATION = certification; isorule.AUDITS = audits; return isorule; }
                    , new {@IDRULE = id},
                    splitOn: "IDCERTIFICATION, IDAUDIT",
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingIsorule(getIsoruleById);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ISORULE> RemoveIsoRule(string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var removeIsoRule = await conn.QueryAsync<ISORULE, CERTIFICATION, AUDITS, ISORULE>("ISO.SP_REMOVE_ISORULE",
                    map: (isorule, certification, audits) => { isorule.CERTIFICATION = certification; isorule.AUDITS = audits; return isorule; }
                    , new { @IDRULE= id},
                    splitOn: "IDCERTIFICATION, IDAUDIT",
                    commandType: CommandType.StoredProcedure);
                conn.Close();
                conn.Dispose();
                var result = MappingIsorule(removeIsoRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<ISORULE> UpdateIsoRule(ISORULE rule, string id)
        {
            try
            {
                using var conn = db.GetConnection();
                conn.Open();
                var updateRule = await conn.QueryAsync<ISORULE, CERTIFICATION, AUDITS, ISORULE>("ISO.SP_UPDATE_ISORULE",
                    map: (isorule, certification, audits) => { isorule.CERTIFICATION = certification; isorule.AUDITS = audits; return isorule; },
                    new
                {
                    @IDRULE = id,
                    @IDAUDIT = rule.IDAUDIT,
                    @IDCERTIFICATION = rule.IDCERTIFICATION,
                    @NAMERULE = rule.NAMERULE,
                    @RULE_DESCRIPTION = rule.RULE_DESCRIPTION
                },
                    splitOn: "IDCERTIFICATION, IDAUDIT",
                    commandType: CommandType.StoredProcedure);
                var result = MappingIsorule(updateRule);
                return result;
            }
            catch (Exception e)
            {

                throw;
            }
        }
        private ISORULE MappingIsorule(IEnumerable<ISORULE> isoRuleList)
        {
            ISORULE isorule = new ISORULE();
            foreach (var item in isoRuleList)
            {
                isorule.IDRULE = item.IDRULE;
                isorule.IDCERTIFICATION = item.IDCERTIFICATION;
                isorule.IDAUDIT = item.IDAUDIT;
                isorule.NAMERULE = item.NAMERULE;
                isorule.RULE_DESCRIPTION = item.RULE_DESCRIPTION;   
                isorule.CREATEDATE = item.CREATEDATE;
                isorule.UPDATEDATE = item.UPDATEDATE;
                isorule.PERSONCHANGE = item.PERSONCHANGE;
                isorule.AUDITS = item.AUDITS;
                isorule.CERTIFICATION = item.CERTIFICATION;
            }
            return isorule;
        }
    }
}
