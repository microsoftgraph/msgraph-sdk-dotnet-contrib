using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Graph.Community
{
	public static class GraphGroupExtensions
	{
		/// <summary>
		/// Update the <see cref="Microsoft.Graph.Group"/> object with the specified directory object as a member
		/// </summary>
		/// <param name="group">The object id of the group</param>
		/// <param name="directoryObjectId">The object id of the user</param>
		/// <remarks>This method is intended to update a Group object that is part of a call to create a group. To manipulate membership of existing groups, use the <see cref="AddAsync"/> or <see cref="RemoveAsync"/> methods.</remarks>
		public static void AddMember(this Microsoft.Graph.Group group, string directoryObjectId)
		{
      if (group is null)
      {
        throw new ArgumentNullException(nameof(group));
      }

      LogExtensionMethod(nameof(AddMember));

			if (group.AdditionalData == null)
			{
				group.AdditionalData = new Dictionary<string, object>();
			}

			string[] membersToAdd = new string[1];
			membersToAdd[0] = $"https://graph.microsoft.com/v1.0/users/{directoryObjectId}";
			group.AdditionalData.Add("members@odata.bind", membersToAdd);
		}

		/// <summary>
		/// Update the <see cref="Microsoft.Graph.Group"/> object with the specified User as an owner
		/// </summary>
		/// <param name="group">The object id of the group</param>
		/// <param name="userId">The object id of the user or service principal</param>
		/// <remarks>This method is intended to update a Group object that is part of a call to create a group. To manipulate membership of existing groups, use the <see cref="AddAsync"/> or <see cref="RemoveAsync"/> methods.</remarks>
		public static void AddOwner(this Microsoft.Graph.Group group, string userId)
		{
			if (group.AdditionalData == null)
			{
				group.AdditionalData = new Dictionary<string, object>();
			}

			LogExtensionMethod(nameof(AddOwner));

			string[] ownersToAdd = new string[1];
			ownersToAdd[0] = $"https://graph.microsoft.com/v1.0/users/{userId}";
			group.AdditionalData.Add("owners@odata.bind", ownersToAdd);
		}

		/// <summary>
		/// Adds the directory object to the Group members collection.
		/// </summary>
		/// <param name="request">The <see cref="IGroupMembersCollectionWithReferencesRequest"/> request that references the Group.</param>
		/// <param name="directoryObjectId">The object id of the directory object to add as a member.</param>
		/// <returns>This is a convenience method for Groups["<groupId>"].Members.Reference.AddAsync()</groupId></returns>
		public static Task AddAsync(this IGroupMembersCollectionWithReferencesRequest request, string directoryObjectId)
		{
      if (request is null)
      {
        throw new ArgumentNullException(nameof(request));
      }

      if (string.IsNullOrEmpty(directoryObjectId))
      {
        throw new ArgumentException($"'{nameof(directoryObjectId)}' cannot be null or empty.", nameof(directoryObjectId));
      }

      LogExtensionMethod(nameof(AddAsync));

			var directoryObject = new DirectoryObject
			{
				Id = directoryObjectId
			};

			var requestUri = new Uri(request.RequestUrl);
			var refUrl = $"{requestUri.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped)}/$ref";

			var referencesBuilder = new GroupMembersCollectionReferencesRequestBuilder(refUrl, request.Client);
			var referencesRequest = referencesBuilder.Request();
	
			return referencesBuilder.Request().AddAsync(directoryObject);
		}

		/// <summary>
		/// Removes the directory object from the Group members collection.
		/// </summary>
		/// <param name="request">The <see cref="IGroupMembersCollectionWithReferencesRequest"/> request that references the Group.</param>
		/// <param name="directoryObjectId">The object id of the directory object to remove.</param>
		/// <returns></returns>
		public static Task RemoveAsync(this IGroupMembersCollectionWithReferencesRequest request, string directoryObjectId)
    {
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (string.IsNullOrEmpty(directoryObjectId))
			{
				throw new ArgumentException($"'{nameof(directoryObjectId)}' cannot be null or empty.", nameof(directoryObjectId));
			}

			LogExtensionMethod(nameof(RemoveAsync));

			var requestUri = new Uri(request.RequestUrl);
			var refUrl = $"{requestUri.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped)}/{directoryObjectId}/$ref";

			var referencesBuilder = new DirectoryObjectWithReferenceRequestBuilder(refUrl, request.Client);
			var referencesRequest = referencesBuilder.Request();

			return referencesBuilder.Request().DeleteAsync();
		}


		/// <summary>
		/// Adds the directory object to the Group owner collection.
		/// </summary>
		/// <param name="request">The <see cref="IGroupOwnersCollectionWithReferencesRequest"/> request that references the Group.</param>
		/// <param name="directoryObjectId">The object id of the directory object to add as an owner.</param>
		/// <returns>This is a convenience method for Groups["<groupId>"].Owners.Reference.AddAsync()</groupId></returns>
		public static Task AddAsync(this IGroupOwnersCollectionWithReferencesRequest request, string directoryObjectId)
		{
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (string.IsNullOrEmpty(directoryObjectId))
			{
				throw new ArgumentException($"'{nameof(directoryObjectId)}' cannot be null or empty.", nameof(directoryObjectId));
			}

			LogExtensionMethod(nameof(AddAsync));

			var directoryObject = new DirectoryObject
			{
				Id = directoryObjectId
			};

			var requestUri = new Uri(request.RequestUrl);
			var refUrl = $"{requestUri.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped)}/$ref";

			var referencesBuilder = new GroupOwnersCollectionReferencesRequestBuilder(refUrl, request.Client);
			var referencesRequest = referencesBuilder.Request();

			return referencesBuilder.Request().AddAsync(directoryObject);
		}

		/// <summary>
		/// Removes the directory object from the Group owners collection.
		/// </summary>
		/// <param name="request">The <see cref="IGroupOwnersCollectionWithReferencesRequest"/> request that references the Group.</param>
		/// <param name="directoryObjectId">The object id of the directory object to remove.</param>
		/// <returns></returns>
		public static Task RemoveAsync(this IGroupOwnersCollectionWithReferencesRequest request, string directoryObjectId)
		{
			if (request is null)
			{
				throw new ArgumentNullException(nameof(request));
			}

			if (string.IsNullOrEmpty(directoryObjectId))
			{
				throw new ArgumentException($"'{nameof(directoryObjectId)}' cannot be null or empty.", nameof(directoryObjectId));
			}

			LogExtensionMethod(nameof(RemoveAsync));

			var requestUri = new Uri(request.RequestUrl);
			var refUrl = $"{requestUri.GetComponents(UriComponents.SchemeAndServer | UriComponents.Path, UriFormat.Unescaped)}/{directoryObjectId}/$ref";

			var referencesBuilder = new DirectoryObjectWithReferenceRequestBuilder(refUrl, request.Client);
			var referencesRequest = referencesBuilder.Request();

			return referencesBuilder.Request().DeleteAsync();
		}


		private static void LogExtensionMethod(string extensionMethodName = "Not specified")
		{
			if (CommunityGraphClientFactory.TelemetryDisabled)
			{
				return;
			}

			Dictionary<string, string> properties = new Dictionary<string, string>(10)
			{
				{ CommunityGraphConstants.Headers.CommunityLibraryVersionHeaderName, CommunityGraphConstants.Library.AssemblyVersion },
				{ CommunityGraphConstants.TelemetryProperties.ExtensionMethod, extensionMethodName },
			};

			TelemetryConfiguration telemetryConfiguration = TelemetryConfiguration.CreateDefault();
			TelemetryClient telemetryClient = new TelemetryClient(telemetryConfiguration);

			telemetryClient.TrackEvent("GraphCommunityExtensionMethod", properties);
			telemetryClient.Flush();


		}
	}
}
