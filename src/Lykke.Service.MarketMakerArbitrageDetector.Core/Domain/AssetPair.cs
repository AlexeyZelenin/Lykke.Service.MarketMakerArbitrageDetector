﻿using System;
using System.Diagnostics;

namespace Lykke.Service.MarketMakerArbitrageDetector.Core.Domain
{
    public class AssetPair
    {
        public string Id { get; }

        public string Name { get; }

        public Asset Base { get; }

        public Asset Quote { get; }
        

        public AssetPair(string id)
        {
            Id = id;
        }

        public AssetPair(string id, string name, Asset @base, Asset quote)
        {
            Id = id;
            Name = name;
            Base = @base;
            Quote = quote;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id) && !string.IsNullOrWhiteSpace(Name) &&
                   Base != null && Base.IsValid() && Quote != null && Quote.IsValid();
        }

        public AssetPair Reverse()
        {
            Debug.Assert(IsValid());

            return new AssetPair(Id, Quote.Name + Base.Name, Quote, Base);
        }

        public bool IsReversed(AssetPair other)
        {
            Debug.Assert(IsValid());
            Debug.Assert(other.IsValid());

            return Base.Id == other.Quote.Id && Quote.Id == other.Base.Id;
        }

        public bool EqualOrReversed(AssetPair other)
        {
            Debug.Assert(IsValid());
            Debug.Assert(other.IsValid());

            return Equals(other) || IsReversed(other);
        }

        public bool ContainsAsset(string assetId)
        {
            Debug.Assert(IsValid());

            if (string.IsNullOrWhiteSpace(assetId))
                throw new ArgumentException(nameof(assetId));

            return Base.Id == assetId || Quote.Id == assetId;
        }

        public bool ContainsAssets(string oneId, string anotherId)
        {
            Debug.Assert(IsValid());

            if (string.IsNullOrWhiteSpace(oneId))
                throw new ArgumentException(nameof(oneId));

            if (string.IsNullOrWhiteSpace(anotherId))
                throw new ArgumentException(nameof(anotherId));

            return ContainsAsset(oneId) && ContainsAsset(anotherId);
        }

        public string GetOtherAssetId(string oneId)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(oneId));
            Debug.Assert(IsValid());

            if (!ContainsAsset(oneId))
                return null;

            var result = Base.Id == oneId ? Quote.Id : Base.Id;

            return result;
        }

        #region Equals and GetHashCode

        public bool Equals(AssetPair other)
        {
            return Base.Id == other.Base.Id && Quote.Id == other.Quote.Id;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is AssetPair other))
                throw new InvalidCastException(nameof(obj));

            return Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Id != null ? Id.GetHashCode() : 0) * 397;
            }
        }

        #endregion

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
