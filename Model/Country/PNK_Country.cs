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
    public class PNK_Country
    {
        #region fields
        private int id;
        private int categoryId;
        private string image;
        private string published;
        private DateTime postDate;
        private DateTime updateDate;
        private int ordering;
        private PNK_CountryDesc countryDesc;
        #endregion

        #region properties
        public PNK_CountryDesc CountryDesc
        {
            get { return countryDesc; }
            set { countryDesc = value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public int CategoryId
        {
            get { return this.categoryId; }
            set { this.categoryId = value; }
        }
        public string Image
        {
            get { return this.image; }
            set { this.image = value; }
        }
        public string Published
        {
            get { return this.published; }
            set { this.published = value; }
        }
        public DateTime PostDate
        {
            get { return this.postDate; }
            set { this.postDate = value; }
        }
        public DateTime UpdateDate
        {
            get { return this.updateDate; }
            set { this.updateDate = value; }
        }
        public int Ordering
        {
            get { return this.ordering; }
            set { this.ordering = value; }
        }
        #endregion

        #region constructor
        public PNK_Country()
        {
            this.id = int.MinValue;
            this.categoryId = int.MinValue;
            this.image = string.Empty;
            this.published = string.Empty;
            this.postDate = DateTime.MinValue;
            this.updateDate = DateTime.MinValue;
            this.ordering = int.MinValue;
            countryDesc = new PNK_CountryDesc();
        }
        public PNK_Country(int id,
                    int categoryId,
                    string image,
                    string published,
                    DateTime postDate,
                    DateTime updateDate,
                    int ordering)
        {
            this.id = id;
            this.categoryId = categoryId;
            this.image = image;
            this.published = published;
            this.postDate = postDate;
            this.updateDate = updateDate;
            this.ordering = ordering;
        }
        #endregion

    }
}