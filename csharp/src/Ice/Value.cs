﻿// **********************************************************************
//
// Copyright (c) 2003-2016 ZeroC, Inc. All rights reserved.
//
// This copy of Ice is licensed to you under the terms described in the
// ICE_LICENSE file included in this distribution.
//
// **********************************************************************

using System;

namespace Ice
{
    [Serializable]
    public abstract class Value : ICloneable
    {
        private const string _id = "::Ice::Object";

        /// <summary>
        /// Returns the Slice type ID of the interface supported by this object.
        /// </summary>
        /// <returns>The return value is always ::Ice::Object.</returns>
        public static string ice_staticId()
        {
            return _id;
        }

        /// <summary>
        /// Returns the Slice type ID of the most-derived interface supported by this object.
        /// </summary>
        /// <returns>The return value is always ::Ice::Object.</returns>
        public virtual string ice_id()
        {
            return _id;
        }

        /// <summary>
        /// The Ice run time invokes this method prior to marshaling an object's data members. This allows a subclass
        /// to override this method in order to validate its data members.
        /// </summary>
        public virtual void ice_preMarshal()
        {
        }

        /// <summary>
        /// This Ice run time invokes this method vafter unmarshaling an object's data members. This allows a
        /// subclass to override this method in order to perform additional initialization.
        /// </summary>
        public virtual void ice_postUnmarshal()
        {
        }

        public virtual void iceWrite(OutputStream ostr)
        {
            ostr.startValue(null);
            iceWriteImpl(ostr);
            ostr.endValue();
        }

        public virtual void iceRead(InputStream istr)
        {
            istr.startValue();
            iceReadImpl(istr);
            istr.endValue(false);
        }

        protected virtual void iceWriteImpl(OutputStream ostr)
        {
        }

        protected virtual void iceReadImpl(InputStream istr)
        {
        }

        /// <summary>
        /// Returns a copy of the object. The cloned object contains field-for-field copies
        /// of the state.
        /// </summary>
        /// <returns>The cloned object.</returns>
        public object Clone()
        {
            return MemberwiseClone();
        }
    }

    public class InterfaceByValue : Value
    {
        public InterfaceByValue(string id)
        {
            _id = id;
        }

        public override string ice_id()
        {
            return _id;
        }

        protected override void iceWriteImpl(OutputStream ostr)
        {
            ostr.startSlice(ice_id(), -1, true);
            ostr.endSlice();
        }

        protected override void iceReadImpl(InputStream istr)
        {
            istr.startSlice();
            istr.endSlice();
        }

        private string _id;
    } 
}
