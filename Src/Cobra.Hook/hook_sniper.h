#pragma once

#include "hook.h"

#include <string>

class SniperHook final : IHook
{
public:
	void InitializeOptions() override;
	void PreInitializeHook() override;
	void PostInitializeHook() override;

private:
	std::string WebServiceUrl;
};
