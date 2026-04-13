using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace umfgcloud.aplicacao.service.testes.Classes
{
    public abstract class AbstractServicoTestes
    {
        private const string C_ROLE_DEFAULT = "default-role";
        private const string C_AUTHENTICATION_AUDIENCE = "altas.mars.net.br";
        private const string C_AUTHENTICATION_ISSUER = "marscloud.atlas.service";

        private const string C_AUTHORIZATION = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJtb2R1bGVzIjoiVG9kb3MiLCJzdWIiOiI0NTUzZmMwNi1mYWM2LTQwMmMtOWVmYy05MDA0ZDhjMjE3" +
            "ZmIiLCJuYW1laWQiOiI2ZTQ3NWFiMS05ODY5LTQ0NWItODJiNy1kMWNkMmZjMmQzMTQiLCJuYW1lIjoi" +
            "TUFSUyIsImVtYWlsIjoiTUFSUy5URUMuQlJAR01BSUwuQ09NIiwianRpIjoiYTA0NmQ3MzQtZjUxOC00" +
            "MTFiLTg1MjAtZjhjYzlmMGYwMThkIiwibmJmIjoxNzczNDA5OTA1LCJpYXQiOjE3NzM0MDk5MDUsInJv" +
            "bGUiOiJBZG1pbmlzdHJhZG9yIiwiZXhwIjoxNzczNDEzNTA1LCJpc3MiOiJtYXJzY2xvdWQuYXRsYXMu" +
            "c2VydmljZSIsImF1ZCI6ImF0bGFzLm1hcnMubmV0LmJyIn0.Yb1CXbTIl93aeOqCP9ioWrLk09hP0Q65" +
            "2l2A4HzDpGw";

        protected const string C_USER_PASSWORD = "123Mudar@";


       
    }
}
