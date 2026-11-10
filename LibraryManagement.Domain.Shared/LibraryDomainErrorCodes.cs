using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagement.Domain.Shared
{
    public static class LibraryDomainErrorCodes
    {
        private const string Prefix = "Library.Error";
        //Kullanıcı Hesabı Hataları(10xxx)
        public const string UserAlreadyExists = $"{Prefix}:10001";
        public const string PasswordShortLength = $"{Prefix}:10002";//
        public const string UserNotFound = $"{Prefix}:10003";//
        public const string UserNotChanged = $"{Prefix}:10004";//
        public const string NoMembershipInfo = $"{Prefix}:10005";
        public const string BorrowLimitExceeded = $"{Prefix}:10006";//
        public const string IncorrectInputs = $"{Prefix}:10007";//
        public const string UserNullInputs = $"{Prefix}:10008";//
        //Kitap Hataları(20xxx)
        public const string BookNotFound = $"{Prefix}:20000";
        public const string BookAlreadyExists = $"{Prefix}:20001";
        public const string BookNotChanged = $"{Prefix}:20002";
        public const string BookCountNegative = $"{Prefix}:20003";
        public const string BookNullInputs = $"{Prefix}:10004";
        //Ödünç Hataları(30xxx)

        public const string BookOutOfStock = $"{Prefix}:30001";
        public const string BorrowRecordNotFound = $"{Prefix}:30002";
        public const string BorrowLimitReached = $"{Prefix}:20004";
        public const string BookAlreadyReturned = $"{Prefix}20005";
        //Ceza Hataları(40xxx)
        public const string FineNotExist = $"{Prefix}:40000";
        public const string NotEnoughMoney = $"{Prefix}:40001";
        public const string BookNotReturned = $"{Prefix}:40002";

    }
}
