namespace TranslateAPI.Entities
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public int NumberIpSystem { get; set; }//tổng IP ở hệ thống
        public int NumberOfUsersSystem { get; set; }//tổng người dùng ở hệ thống
        public int NumberOfUsesSystem { get; set; }//tổng lượt đã dùng ở hệ thống
        public double NumberOfCoinSystem { get; set; }//tổng số tiền ở hệ thống
        public double NumberOfUsedCoin { get; set; }//tổng số tiền các user đã sử dụng
        public double NumberOfRemainingCoin { get; set; }//tổng số tiền còn lại của user
    }
}
