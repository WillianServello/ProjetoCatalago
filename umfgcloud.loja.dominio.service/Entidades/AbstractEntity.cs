using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umfgcloud.loja.dominio.service.Entidades
{
    public abstract class AbstractEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CreatedByUserId{ get; set; } = string.Empty;
        public string CreatedByUserEmail{ get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string UpdateByUserId{ get; set; } = string.Empty;
        public string UpdateByUserEmail{ get; set; } = string.Empty;
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        protected AbstractEntity() { }

        protected AbstractEntity(string userId, string userEmail) 
        {
            CreatedByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
            CreatedByUserEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
            UpdateByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
            UpdateByUserEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
        }

        public virtual void Activate() => IsActive = true;
        public virtual void Inactivate() => IsActive = false;

        public void Update(string userId, string userEmail)
        {
            UpdateByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
            UpdateByUserEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
            CreateAt = DateTime.UtcNow;
        }
    }
}
