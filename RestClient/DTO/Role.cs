namespace RestClient.DTO
{
    /// <summary>
    /// Data transfer object representing a role from the service owner API.
    /// </summary>
    [PluralName("Roles")]
    public class Role : HalJsonResource
    {
        /// <summary>
        /// Gets or sets the role type.
        /// </summary>
        public string RoleType { get; set; }

        /// <summary>
        /// Gets or sets the role definition id.
        /// </summary>
        public string RoleDefinitionId { get; set; }

        /// <summary>
        /// Gets or sets the role name.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the role description.
        /// </summary>
        public string RoleDescription { get; set; }

    }
}
