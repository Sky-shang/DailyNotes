    //----------tb_User开始----------
    
    using System;
    namespace MyProject.Entities 
    {
        /// <summary>
        /// 数据表实体类：tb_User 
        /// </summary>
        [Serializable()]
        public class tb_User
        {    
                         
            /// <summary>
            /// Int32:
            /// </summary>                       
            public Int32 UserID {get;set;}   
                         
            /// <summary>
            /// String:
            /// </summary>                       
            public String UserName {get;set;}   
                         
            /// <summary>
            /// String:
            /// </summary>                       
            public String PassWord {get;set;}   
                         
            /// <summary>
            /// String:
            /// </summary>                       
            public String Email {get;set;}   
                         
            /// <summary>
            /// Boolean:
            /// </summary>                       
            public Boolean Role {get;set;}   
               
        }    
     }

    //----------tb_User结束----------

    