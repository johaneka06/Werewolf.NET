using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Werewolf.NET.Game;
using Werewolf.NET.Game.Database.PostgreSQL;
using Npgsql;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public RoleController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<DBRole> Get()
        {
            DotNetEnv.Env.Load();
            postgreUnitOfWork unit = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));

            List<DBRole> roles = unit.RoleRepo.GetAllRoles();

            return roles;
        }

        [HttpPost]
        public ActionResult Post([FromBody] Roles newRole)
        {
            DotNetEnv.Env.Load();
            postgreUnitOfWork db = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));

            db.RoleRepo.Create(newRole);
            db.Commit();

            return Ok();
        }

        [HttpPut]
        public DBRole Put([FromBody] UpdateRole updateRole)
        {
            DotNetEnv.Env.Load();
            postgreUnitOfWork db = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));

            DBRole newRole = new DBRole(updateRole.id, updateRole.name, DateTime.Now, DateTime.Now);

            DBRole updated = db.RoleRepo.UpdateRole(newRole);
            db.Commit();

            return updated;
        }

        [HttpGet("byId")]
        public DBRole GetRole([FromQuery] int id = 1)
        {
            DotNetEnv.Env.Load();
            postgreUnitOfWork db = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));

            return db.RoleRepo.GetRoles(id);
        }

        [HttpDelete("byId")]
        public ActionResult Delete([FromQuery] int id)
        {
            DotNetEnv.Env.Load();
            postgreUnitOfWork db = new postgreUnitOfWork(System.Environment.GetEnvironmentVariable("CONN_STR"));

            if (db.RoleRepo.DeleteRole(id) == null) return NotFound();
            else
            {
                db.Commit();
                return Ok();
            }
        }

    }

    public class UpdateRole
    {
        public int id { set; get; }
        public string name { set; get; }
    }
}