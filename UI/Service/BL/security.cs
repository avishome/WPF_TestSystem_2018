using System;
using System.Collections.Generic;
using WCFServiceWebRole1.BE;

namespace WCFServiceWebRole1.BL
{
    public static class Security
    {
        /// <summary>
        /// check if user friend of interfaceacsess object.
        /// </summary>
        /// <param name="groups">interfaceacsess object gruop</param>
        /// <param name="permiss">one user permission in his list</param>
        /// <returns></returns>
        public static bool permiitionOnListPermittion(List<string> groups, permission permiss)
        {
            foreach (string val in groups)
                if (val == permiss.Name) return true;
            return false;
        }
        /// <summary>
        /// check if the user can play num code in his permission
        /// </summary>
        /// <param name="num">def.op of try action</param>
        /// <param name="permiss">one user permission in his list</param>
        /// <returns></returns>
        public static bool acsessToFunc(int num, permission permiss)
        {
            bool x;
            try { x = permiss.types[num]; }
            catch (Exception e) { throw new Exception("maybe the op code don't in the objct??" + e); }
            return (x);
        }
        /// <summary>
        /// check for all permission of user if there is apropriate permission to action
        /// </summary>
        /// <param name="hash">user code</param>
        /// <param name="op">action code</param>
        /// <param name="x">object gruops</param>
        public static void HashToAcsess(string hash, int op, List<string> x)
        {
            string id = DL.Dal_imp.Instance.idByToken(hash);
            ListPermission L = DL.Dal_imp.Instance.ListpermissById(id);
            foreach (permission P in L.permisses)
            {
                if (permiitionOnListPermittion(x, P) && acsessToFunc(op, P)) { return; }
            }
            throw new Exception("acsess deined");
        }
    }
}

