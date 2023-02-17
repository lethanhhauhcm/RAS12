Module mdlDeclare
    Public myStaff As New objStaff
    Public MySession As New objTerminal
    Public MyAL As New objAL

    Public Conn As New SqlClient.SqlConnection
    Public Conn_Web As New SqlClient.SqlConnection
    Public Conn_TVVN As New SqlClient.SqlConnection
    Public ConnF1S As New SqlClient.SqlConnection
    Public ConnAOP As New SqlClient.SqlConnection
    'Public Conn_Web  As New SqlClient.SqlConnection

    Public pobjTvcs As New clsTvcs

    Public CnStr As String = ""
    'Public Const CnStr_RasHAN As String = "server=42.117.5.70;uid=user_ras;pwd=VietHealthy@170172#;database=lib" ' Update cac bang Lib chung
    Public Const CnStr_FT As String = "server=118.69.81.103;uid=user_ft;pwd=VietHealthy@170172#;database=FT" ' chck Balance
    Public Const CnStr_TVVN As String = "server=118.69.81.103;uid=user_transvietvn;pwd=VietHealthy@170172#;database=transvietvn" ' Mapp User webReport
    Public Const CnStr_F1S As String = "SERVER=118.69.81.103;uid=user_f1s;pwd=VietHealthy@170172#;database=f1s" ' tao User cho F1S
    Public Const CnStr_FLX As String = "Data Source=118.69.81.103;Initial Catalog=FLX;UID=user_flx;Pwd=VietHealthy@170172#;TimeOut=30" ' Theo doi Qtuan
    'Public Const CnStr_AOP As String = "Data Source=172.16.2.6; User Id= user_aop; Password= VietHealthy@170172#; Connection Timeout=10;"
    Public Const CnStr_AOP As String = "Data Source=118.69.68.197; User Id= user_aop; Password= VietHealthy@170172#; Connection Timeout=30;"

    Public Const msgTitle As String = "TransViet Travel :: RAS"
    Public pubVarSRV As String = ""
    Public pubVarRCPID_BeingEdited As Integer = 0
    Public pubVarRCPID_BeingCreated As Integer = 0
    Public pubVarBackColor As Color

    Public CutOverDatePPD As Date
    Public CutOverDatePSP As Date
    Public CutOverDateCloseRPT As Date

    Public DDAN As String
    Public pstrPrg As String = "RAS"

    Public pstrVnDomCities As String

    Public Const DKDataConvertMktg_RAS As String = " from TKT t inner join rcp r on t.rcpid=r.recid and r.status not in ('XX','QQ','NA') " _
        & "and t.RecID not in (select TKID from ReportData) " _
        & " and t.status<>'XX' and t.srv <>'V' " _
        & " and ((t.al <>'XX' and doctype='ATK') or  (t.al not in ('XX','01') and doctype not in ('GRP','SST')))" _
        & " And doi >'01 JAN 2017'   and (sbu='TVS' or fare+tax+t.charge <>0 )"

    Public Const DKDataConvertMktg_BSP As String = " from UA_Hot where ID not in (select TKID from ReportData_BSP) " &
        "and tdnr <>'' and dais<>''"
    Public pblnLogXml As Boolean
    Public pstrIata As String = "37301401"
    Public pobjCustomer As New clsCustomer
    Public pstrTravelID_forHTL As String

    Public Enum FrontBackOffc
        FrontOffice = 1
        BackOffice = 2
    End Enum
End Module
