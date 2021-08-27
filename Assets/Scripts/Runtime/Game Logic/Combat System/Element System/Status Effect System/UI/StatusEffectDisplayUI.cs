using System.Collections.Generic;
using UnityEngine;

namespace WizardGame.Combat_System.Element_System.Status_Effects
{
    public class StatusEffectDisplayUI : MonoBehaviour
    {
        [Header("Functional references")]
        [SerializeField] private StatusEffectHandler statEffHandler = default;
        [SerializeField] private StatusEffectListingUI listingPrefab = default;

        [Header("UI references")] 
        [SerializeField] private Transform gridTransform;
        
        private readonly Dictionary<StatusEffectData, StatusEffectListingUI> existingListings
            = new Dictionary<StatusEffectData, StatusEffectListingUI>();

        private void Awake()
        {
            statEffHandler.onStatEffAdded += OnStatusEffectAdded;
            statEffHandler.onStatEffRemoved += OnStatusEffectRemoved;
        }

        private void OnStatusEffectAdded(StatusEffectData data, StatusEffectBase statEff)
        {
            if (existingListings.ContainsKey(data))
            {
                existingListings[data].AddStatusEffect(statEff);
            }
            else
            {
                var listing = CreateListing();
                listing.UpdateImageIcon(data.StatusEffectIcon);
                
                existingListings.Add(data, listing);
                
                OnStatusEffectAdded(data, statEff);
            }
        }

        private void OnStatusEffectRemoved(StatusEffectData data, StatusEffectBase statEff)
        {
            if (!existingListings.ContainsKey(data)) return;

            var toRemoveListing = existingListings[data].RemoveStatusEffect(statEff);

            if (toRemoveListing)
            {
                Destroy(existingListings[data].gameObject);

                existingListings.Remove(data);
            }
        }

        private StatusEffectListingUI CreateListing()
        {
            var listingClone = Instantiate(listingPrefab, gridTransform);

            return listingClone;
        }
    }
}