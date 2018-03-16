        private static MemberInfo FindInnerMemberAndInject<TService, TImplement>(
            TImplement instance,
            Func<TService> next)
        {
            var typeInfo = typeof(TImplement).GetTypeInfo();
            var nextProperty = typeInfo
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(e => e.SetMethod != null && e.PropertyType == typeof(TService));
            if (nextProperty != null)
            {
                nextProperty.SetValue(instance, next());
                return nextProperty;
            }

            var nextField = typeInfo
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(e => e.FieldType == typeof(TService));
            if (nextField != null)
            {
                nextField.SetValue(instance, next());
                return nextField;
            }

            return null;
        }
    }
