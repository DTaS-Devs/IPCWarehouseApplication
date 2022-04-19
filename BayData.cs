using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCWarehouseApplication
{
    class BayData
    {

        public struct Bay_Data
        {
            //Bay Information
            private string _biid;
            private string _bireference;
            private string _bistarttime;
            private string _biendtime;
            private string _biawb;
            private string _biitemlines;
            private string _bipallets;
            private string _bicartons;

            //Container Information
            private string _cicontainerno;
            private string _cicomments;

            //Border Force
            private string _bfdm;
            private string _bftl;
            private string _bfexamteam;

            //Examination
            private string _elead;
            private string _eexamteam;

            //Additional Information
            private string _aiinfo;

            //Sampling
            private string _snotebook;
            private string _sphoto;
            private string _scage;

            public string BIID
            {
                get
                {
                    return _biid;
                }
                set
                {
                    _biid = value;
                }
            }

            public string BIReference
            {
                get
                {
                    return _bireference;
                }
                set
                {
                    _bireference = value;
                }
            }

            public string BIStartTime
            {
                get
                {
                    return _bistarttime;
                }
                set
                {
                    _bistarttime = value;
                }
            }

            public string BIEndTime
            {
                get
                {
                    return _biendtime;
                }
                set
                {
                    _biendtime = value;
                }
            }

            public string BIAWB
            {
                get
                {
                    return _biawb;
                }
                set
                {
                    _biawb = value;
                }
            }

            public string BIItemLines
            {
                get
                {
                    return _biitemlines;
                }
                set
                {
                    _biitemlines = value;
                }
            }

            public string BIPallets
            {
                get
                {
                    return _bipallets;
                }
                set
                {
                    _bipallets = value;
                }
            }

            public string BICartons
            {
                get
                {
                    return _bicartons;
                }
                set
                {
                    _bicartons = value;
                }
            }

            public string CIContainerNo
            {
                get
                {
                    return _cicontainerno;
                }
                set
                {
                    _cicontainerno = value;
                }
            }

            public string CIComments
            {
                get
                {
                    return _cicomments;
                }
                set
                {
                    _cicomments = value;
                }
            }

            public string BFDM
            {
                get
                {
                    return _bfdm;
                }
                set
                {
                    _bfdm = value;
                }
            }

            public string BFTL
            {
                get
                {
                    return _bftl;
                }
                set
                {
                    _bftl = value;
                }
            }

            public string BFExamTeam
            {
                get
                {
                    return _bfexamteam;
                }
                set
                {
                    _bfexamteam = value;
                }
            }

            public string ELead
            {
                get
                {
                    return _elead;
                }
                set
                {
                    _elead = value;
                }
            }

            public string EExamTeam
            {
                get
                {
                    return _eexamteam;
                }
                set
                {
                    _eexamteam = value;
                }
            }

            public string AIInfo
            {
                get
                {
                    return _aiinfo;
                }
                set
                {
                    _aiinfo = value;
                }
            }

            public string SNotebook
            {
                get
                {
                    return _snotebook;
                }
                set
                {
                    _snotebook = value;
                }
            }

            public string SPhoto
            {
                get
                {
                    return _sphoto;
                }
                set
                {
                    _sphoto = value;
                }
            }

            public string SCage
            {
                get
                {
                    return _scage;
                }
                set
                {
                    _scage = value;
                }
            }
        }
    }
}
