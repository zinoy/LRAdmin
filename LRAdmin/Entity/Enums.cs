using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRAdmin.Entity
{
    /// <summary>
    /// 表示权限种类的标志枚举。
    /// </summary>
    [Flags]
    public enum AccessMethod : short
    {
        /// <summary>
        /// 无权限。
        /// </summary>
        None = 0,
        /// <summary>
        /// 查看列表权限。
        /// </summary>
        ListView = 1,
        /// <summary>
        /// 查看详细权限。
        /// </summary>
        DetailView = 2,
        /// <summary>
        /// 新建权限。
        /// </summary>
        Create = 4,
        /// <summary>
        /// 修改权限。
        /// </summary>
        Edit = 8,
        /// <summary>
        /// 删除权限。
        /// </summary>
        Delete = 16
    }
    /// <summary>
    /// 指示用户状态的枚举。
    /// </summary>
    public enum UserStatus : byte
    {
        /// <summary>
        /// 正常。
        /// </summary>
        Normal = 1,
        /// <summary>
        /// 已禁用。
        /// </summary>
        Banned = 2,
        /// <summary>
        /// 已删除。
        /// </summary>
        Deleted = 3
    }
}
