using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace intcom.web
{
    public class AWS
    {

        static string _bucketName = "*** bucket name ***";
        static string _keyName = "*** key name when object is created ***";

        static IAmazonS3 client;

        public AWS( string bucketName, string keyName)
        {
            _bucketName = bucketName;
            _keyName = keyName;

            TransferUtility fileTransferUtility = new
                    TransferUtility(new AmazonS3Client(Amazon.RegionEndpoint.USEast1));
        }

        public bool Upload(HttpPostedFileBase postedFile)
        {
            using (client = new AmazonS3Client(Amazon.RegionEndpoint.USEast1))
            {
                return WritingAnObject(postedFile);
            }
        }

        static bool WritingAnObject(HttpPostedFileBase postedFile)
        {
            try
            {
                PutObjectRequest putRequest1 = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = _keyName,
                    ContentType = "multipart/form-data"
                };
                putRequest1.InputStream = postedFile.InputStream;

                PutObjectResponse response1 = client.PutObject(putRequest1);

                return true;
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (amazonS3Exception.ErrorCode != null &&
                    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId")
                    ||
                    amazonS3Exception.ErrorCode.Equals("InvalidSecurity")))
                {
                }
                else
                {
                }

                return false;
            }
        }
    }
}