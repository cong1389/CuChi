/**
                             * @version $Id:
                             * @package Digicom.NET
                             * @author Digicom Dev <dev@dgc.vn>
                             * @copyright Copyright (C) 2011 by Digicom. All rights reserved.
                             * @link http://www.dgc.vn
                            */

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;

namespace Cb.Model
{
    public class PNK_Countries_Cities
    {
        #region fields
        private int id;
        private int countryId;
        private string country;
        private string iSO2;
        private string iSO3;
        private string city;
        private double lat;
        private double lng;
        private double pOP;
        private string province;
        private int phoneCode;
        #endregion

        #region properties
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int CountryId
        {
            get { return this.countryId; }
            set { this.countryId = value; }
        }
        public string Country
        {
            get { return this.country; }
            set { this.country = value; }
        }
        public string ISO2
        {
            get { return this.iSO2; }
            set { this.iSO2 = value; }
        }
        public string ISO3
        {
            get { return this.iSO3; }
            set { this.iSO3 = value; }
        }
        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }
        public double Lat
        {
            get { return this.lat; }
            set { this.lat = value; }
        }
        public double Lng
        {
            get { return this.lng; }
            set { this.lng = value; }
        }
        public double POP
        {
            get { return this.pOP; }
            set { this.pOP = value; }
        }
        public string Province
        {
            get { return this.province; }
            set { this.province = value; }
        }
        public int PhoneCode
        {
            get { return this.phoneCode; }
            set { this.phoneCode = value; }
        }
        #endregion

        #region constructor
        public PNK_Countries_Cities()
        {
            this.id = int.MinValue;
            this.countryId = int.MinValue;
            this.country = string.Empty;
            this.iSO2 = string.Empty;
            this.iSO3 = string.Empty;
            this.city = string.Empty;
            this.lat = double.MinValue;
            this.lng = double.MinValue;
            this.pOP = double.MinValue;
            this.province = string.Empty;
            this.phoneCode = int.MinValue;
        }
        public PNK_Countries_Cities(int id,
                    int countryId,
                    string country,
                    string iSO2,
                    string iSO3,
                    string city,
                    double lat,
                    double lng,
                    double pOP,
                    string province,
                    int phoneCode)
        {
            this.id = id;
            this.countryId = countryId;
            this.country = country;
            this.iSO2 = iSO2;
            this.iSO3 = iSO3;
            this.city = city;
            this.lat = lat;
            this.lng = lng;
            this.pOP = pOP;
            this.province = province;
            this.phoneCode = phoneCode;
        }
        #endregion

    }
}